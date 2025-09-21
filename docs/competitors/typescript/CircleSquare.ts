import * as readline from 'readline'
const readlineInterface = readline.createInterface({
    input: process.stdin,
    output: process.stdout
})

const computeCircleArea = (radius: number): string => (Math.PI * radius ** 2).toFixed(3);

const isPositiveNumber = (number: number): boolean => !isNaN(number) && number > 0;

readlineInterface.question("Введите радиус круга: ", (value: string) => {
    const radius: number = parseFloat(value);
    if (isPositiveNumber(radius)) {
        console.log(computeCircleArea(radius))
    } else {
        console.error("Некорректный радиус!")
    }
    readlineInterface.close()
})