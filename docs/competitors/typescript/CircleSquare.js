"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var readline = require("readline");
var readlineInterface = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});
var computeCircleArea = function (radius) { return (Math.PI * Math.pow(radius, 2)).toFixed(3); };
var isPositiveNumber = function (number) { return !isNaN(number) && number > 0; };
readlineInterface.question("Введите радиус круга: ", function (value) {
    var radius = parseFloat(value);
    if (isPositiveNumber(radius)) {
        console.log(computeCircleArea(radius));
    }
    else {
        console.error("Некорректный радиус!");
    }
    readlineInterface.close();
});
