let output, text, websocket;

window.onload = main;

function main() {
  button = document.getElementById("b1");
  output = document.getElementById("output");
  text = document.getElementById("area");
  button.addEventListener("click", onClickButton);
}

function writeToScreen(message) {
  output.insertAdjacentHTML("afterbegin", "<p>" + message + "</p>");
}

function onClickButton() {
  if (text.value) {
    websocket = new WebSocket("ws://127.0.0.1/");

    websocket.onopen = function () {
      writeToScreen("SENT: " + text.value);
      websocket.send(text.value);
      text.value = "";
      text.focus();
    };

    websocket.onmessage = function (e) {
      writeToScreen("<span>RESPONSE: " + e.data + "</span>");
    };

    websocket.onerror = function () {
      writeToScreen("<span class=error>ERROR</span>");
    };
  }
}
