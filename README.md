# Объявление ассоциативного массива с инициализацией языка JavaScript 15 вариант
## Примеры допустимых строк
```JS
let arr = { "apple": 10, "grapes": 20 };
let cities = { "Омск": "55 регион", "Кемерово": "42 регион", "Томск":"70 регион"  };
```
## Разработанная грамматика
```
1)	<START> -> 'let' <LET>
2)	<LET> -> string <IDENTIFIER>
3)	<IDENTIFIER> -> '=' <ASSIGNMENT>
4)	<ASSIGNMENT> -> '{' <OPEN>
5)	<OPEN> -> '“' <STRING>
6)	<STRING> -> letter <STRINGREM>
7)	<STRINGREM -> (letter | ‘_’ | digit) <STRINGREM>
8)	<STRINGREM> -> '“' <ENDSTRING>
9)	<ENDSTRING> -> ':' <COLON>
10)	<COLON> -> number <DIGIT>
11)	<COLON> -> '“' <STRING2>
12)	<STRING2> -> letter <STRINGREM2>
13)	<STRINGREM2> -> (letter | ‘_’ | digit) <STRINGREM2>
14)	<STRINGREM2> -> '“' <ENDSTRING2>
15)	<ENDSTRING2> -> ',' <OPEN>
16)	<ENDSTRING2> -> '}' <END>
17)	<DIGIT> -> ',' <OPEN>
18)	<DIGIT> -> '}' <END>
19)	<END> -> ;
```
## Граф конечного автомата
![Документ2 (3)](https://github.com/IceArcher200/CompilerLab1/assets/82698823/8b2b7221-8e50-45e7-ab93-960958193f91)
## Тестовые примеры
![image](https://github.com/IceArcher200/CompilerLab1/assets/82698823/62581ce7-678c-4645-85a4-fa0596d1df95)
![image](https://github.com/IceArcher200/CompilerLab1/assets/82698823/1163e667-b537-4501-a6b3-052b171b90bc)








