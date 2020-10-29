using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace WebsocketLib
{
    /// <summary>
    /// TCP Util Lib
    /// read and write bytes over Networkstream
    /// Follows Internet standards track protocol rfc6455 #section-5.2
    /// </summary>
    public static class Lib
    {
        public static byte[] Read(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            using MemoryStream TmpStream = new MemoryStream();
            while (!stream.DataAvailable) ;

            // read steam to buffer in chunks of 2KB
            byte[] buffer = new byte[2048];
            while (client.Available > 0)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                TmpStream.Write(buffer, 0, bytesRead);
            }
            return TmpStream.ToArray();
        }

        public static void Write(TcpClient client, string msg, bool masked)
        {
            NetworkStream stream = client.GetStream();

            // send in chunks of 2KB
            var parts = GetBytes(msg, 2048, masked);
            for (int i = 0; i < parts.Count; i++)
            {
                stream.Write(parts[i], 0, parts[i].Length);
            }
        }

        public static bool Write(TcpClient client, byte[] bytes)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
            return true;
        }

        private static List<byte[]> GetBytes(string msg, int size = 2048, bool masked = true)
        {
            List<byte[]> chunks;
            int offset = size < 126 ? 2 : 4;
            offset += masked ? 4 : 0;

            // split into chunks
            if (msg.Length > size - offset)
            {
                List<byte> buf = new List<byte>(Encoding.UTF8.GetBytes(msg));

                chunks = buf.Select((x, i) => new { Value = x, Index = i })
                .GroupBy(x => x.Index / (size - offset))
                .Select(x => x.Select(y => y.Value).ToArray())
                .ToList();
            }
            else
                chunks = new List<byte[]>() { Encoding.UTF8.GetBytes(msg) };

            for (int i = 0; i < chunks.Count; i++)
            {
                byte cmd = 0;
                if (i == 0) cmd |= 1;
                if (i == chunks.Count - 1) cmd |= 0x80;
                chunks[i] = EncodeBytes(chunks[i], cmd, masked);
            }
            return chunks;
        }

        public static string DecodeBytes(byte[] bytes)
        {
            bool masked = bytes[1] > 127;
            int msglen = masked ? bytes[1] - 128 : bytes[1];
            int offset = 2;

            if (msglen == 126)
            {
                msglen = BitConverter.ToUInt16(new byte[] { bytes[3], bytes[2] }, 0);
                offset = 4;
            }

            byte[] decoded = new byte[msglen];
            if (masked)
            {
                byte[] masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
                offset += 4;
                for (int i = 0; i < msglen; ++i)
                    decoded[i] = (byte)(bytes[offset + i] ^ masks[i % 4]);

            }
            else
            {
                Buffer.BlockCopy(bytes, offset, decoded, 0, msglen);
            }
            return Encoding.UTF8.GetString(decoded);
        }

        private static byte[] EncodeBytes(byte[] b, byte cmd, bool masked)
        {
            int msglen = b.Length;
            int offset = 2;
            offset += masked ? 4 : 0;

            byte[] msglenBytes = null;
            if (msglen > 126)
            {
                msglenBytes = BitConverter.GetBytes(msglen);
                msglen = 126;
                offset += 2;
            }

            byte[] bytes = new byte[b.Length + offset];
            bytes[0] = cmd;
            bytes[1] = masked ? (byte)(msglen + 128) : (byte)(msglen);

            if (msglen == 126)
            {
                bytes[2] = msglenBytes[1];
                bytes[3] = msglenBytes[0];
            }
            if (masked)
            {
                // Generate  4 random mask bytes.
                var rand = new Random();
                byte[] encoded = new byte[msglen];
                byte[] masks = new byte[4];
                rand.NextBytes(masks);
                for (int i = 0; i < msglen; ++i)
                    encoded[i] = (byte)(b[i] ^ masks[i % 4]);

                Buffer.BlockCopy(masks, 0, bytes, offset - 4, 4);
                Buffer.BlockCopy(encoded, 0, bytes, offset, encoded.Length);
            }
            else
                Buffer.BlockCopy(b, 0, bytes, offset, b.Length);

            return bytes;
        }
    }
}
