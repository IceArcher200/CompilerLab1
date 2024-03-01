# CompilerLab2
Постановка задачи:

1) Спроектировать диаграмму состояний сканера (примеры диаграмм представлены в прикрепленных файлах).

2) Разработать лексический анализатор, позволяющий выделить в тексте лексемы, иные символы считать недопустимыми (выводить ошибку).

3) Встроить сканер в ранее разработанный интерфейс текстового редактора. Учесть, что текст для разбора может состоять из множества строк.

Вариант 15:
Объявление ассоциативного массива с инициализацией языка JavaScript

Ввод:

let arr = { "apple": 10, "grapes": 20 }; 

Вывод:

Token: { Type: "keyword", Value: "let", Position: { Index: "0", Line: "1", Column: "0" } }

Token: { Type: "IDENTIFIER", Value: "arr", Position: { Index: "4", Line: "1", Column: "4" } }

Token: { Type: "ASSIGNMENT", Value: "=", Position: { Index: "8", Line: "1", Column: "8" } }

Token: { Type: "OPEN_BRACE", Value: "{", Position: { Index: "10", Line: "1", Column: "10" } }

Token: { Type: "LINE", Value: ""apple"", Position: { Index: "12", Line: "1", Column: "12" } }

Token: { Type: "Colon", Value: ":", Position: { Index: "19", Line: "1", Column: "19" } }

Token: { Type: "NUMBER", Value: "10", Position: { Index: "21", Line: "1", Column: "21" } }

Token: { Type: "COMMA", Value: ",", Position: { Index: "23", Line: "1", Column: "23" } }

Token: { Type: "LINE", Value: ""grapes"", Position: { Index: "25", Line: "1", Column: "25" } }

Token: { Type: "Colon", Value: ":", Position: { Index: "33", Line: "1", Column: "33" } }

Token: { Type: "NUMBER", Value: "20", Position: { Index: "35", Line: "1", Column: "35" } }

Token: { Type: "CLOSE_BRACE", Value: "}", Position: { Index: "38", Line: "1", Column: "38" } }

Token: { Type: "END_MASSIVE", Value: ";", Position: { Index: "39", Line: "1", Column: "39" } }

<img width="537" alt="Документ (1)" src="https://github.com/IceArcher200/CompilerLab1/assets/82698823/307965db-17fb-40b7-8637-0ca98164eb9b">





