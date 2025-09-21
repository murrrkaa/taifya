import * as readline from 'readline'
const readlineInterface = readline.createInterface({
    input: process.stdin,
    output: process.stdout
})

const isPositiveNumber = (number: number): boolean => !isNaN(number) && number > -1;

const factorial = (number: number): number => {
    if (number === 0 || number === 1) return 1;
    return number * factorial(number - 1)
}

readlineInterface.question("Введите целое число: ", (value: string) => {
    const number: number = parseInt(value)
    if (isPositiveNumber(number)){
        console.log(factorial(number))
    } else {
        console.error("Некорректный ввод!")
    }
    readlineInterface.close()
})