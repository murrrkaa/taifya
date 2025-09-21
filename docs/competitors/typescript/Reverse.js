"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var readline = require("readline");
var readlineInterface = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});
readlineInterface.question("Введите строку: ", function (value) {
    var reversedString = value.trim().split('').reverse().join('');
    console.log(reversedString);
    readlineInterface.close();
});
