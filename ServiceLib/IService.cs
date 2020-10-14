using CarLib;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLib
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int GetAA();

        [OperationContract]
        List<PKW> GetAutos();

        [OperationContract]
        bool AddAuto(PKW _Auto);
    }
}
