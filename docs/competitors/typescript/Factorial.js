"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var readline = require("readline");
var readlineInterface = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});
var isPositiveNumber = function (number) { return !isNaN(number) && number > -1; };
var factorial = function (number) {
    if (number === 0 || number === 1)
        return 1;
    return number * factorial(number - 1);
};
readlineInterface.question("Введите целое число: ", function (value) {
    var number = parseInt(value);
    if (isPositiveNumber(number)) {
        console.log(factorial(number));
    }
    else {
        console.error("Некорректный ввод!");
    }
    readlineInterface.close();
});
