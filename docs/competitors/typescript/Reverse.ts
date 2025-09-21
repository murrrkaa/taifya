import * as readline from 'readline'
const readlineInterface = readline.createInterface({
    input: process.stdin,
    output: process.stdout
})

readlineInterface.question("Введите строку: ", (value: string) => {
    const reversedString: string = value.trim().split('').reverse().join('');
    console.log(reversedString);
    readlineInterface.close()
})