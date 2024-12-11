import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
  .withAutomaticReconnect()
  .withUrl("https://localhost:7034/notifications")
  .build();
export default {
  start() {

    return connection.start();
  },
  on(eventName, callback) {
    connection.on(eventName, callback);
  },
  off(eventName, callback) {
    connection.off(eventName, callback);
  },
  invoke(methodName, ...args) {
    return connection.invoke(methodName, ...args);
  },

};
