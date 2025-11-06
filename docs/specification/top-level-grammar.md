# Спецификация грамматики верхнего уровня языка программирования

## Примеры кода

```
// --- Объявление переменных и констант ---
var x = 10;
var y = -3;
const z = 80;

// --- Присваивание и использование выражений ---
var z = x + y;
print(z);

// --- Ввод числа с клавиатуры и вывод результата ---
var a = readInt();
print(a * a);

// --- Определение пользовательской функции ---
function add(p, q) {
  return p + q;
};

// --- Вызов пользовательской функции ---
var sum = add(x, y);
print(sum);

```

## Ключевые особенности

* Поддерживаются последовательное выполнение инструкций и ввод/вывод через встроенные функции `print` и `readInt`.
* Язык поддерживает глобальную и локальную область видимости внутри функций.

## Семантические правила

1. Все переменные и константы должны быть объявлены перед использованием.
2. Константы не могут изменяться после инициализации.
3. Повторное объявление переменной или константы с тем же именем в одной области действия запрещено.
4. Использование переменной вне области видимости является ошибкой.
5. Функции должны быть объявлены перед вызовом с помощью ключевого слова `function`.
6. Константа (const) должна быть немедленно инициализирована значением в момент объявления.

## Грамматика верхнего уровня

```ebnf

(* Программа находится в одном файле *)
program = top_level_statement, { top_level_statement } ;

(* Верхнеуровневая инструкция *)
top_level_statement =
      function_definition
    | (variable_declaration | constant_declaration | statement), ";" ;

(* Объявление функции *)
function_definition =
    "function", identifier, "(", [ parameter_list ], ")",
    "{", { function_statement }, "}" ;

parameter_list = identifier, { ",", identifier } ;

(* Инструкции внутри функций *)
function_statement =
      assignment_statement
    | function_call_statement
    | variable_declaration
    | constant_declaration
    | print_statement
    | return_statement ;

(* Return только внутри функций *)
return_statement = "return", [ expression ] ;

(* Объявление переменной *)
variable_declaration = "var", identifier, "=", expression ;

(* Объявление константы *)
constant_declaration = "const", identifier, "=", expression ;

(* Присваивание *)
assignment_statement = identifier, "=", expression ;

(* Инструкции *)
statement =
      assignment_statement
    | function_call_statement
    | variable_declaration
    | constant_declaration
    | print_statement ; 
       
(* Вызов функции *)
function_call_statement = (built_in_function | identifier), "(", [ expression_list ], ")" ;

(* Ввод/вывод *)
print_statement = "print", "(", [ expression_list ], ")" ;
```

## Примеры программ

### SumNumbers

```
    var x = readInt();
    var y = readInt();
    var result = x + y;
    print(result);
```

### CircleSquare

```
    const PiValue = 3.1415926535;
    var radius = readInt();
    var area = PiValue * radius * radius;
    print(area); 
```

### FahrenheitToCelsius

```
    var fahrenheit = readInt();
    var celsius = (fahrenheit - 32) * 5 / 9;
    print(celsius);
```