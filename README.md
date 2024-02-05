# CSV4net
"CSV4net" is a CSV file reading library for .net.
"CSV4net" is compiled with .net6.

## Overview
- By specifying the path of a CSV file, read the file data.
- Even values ​​containing line breaks enclosed in double quotes are read correctly..

## Usage
1. Add dll<br>
    Add the dll to your project
2. Add using<br>
    Add the following description
    ```
    using CSV4net;
    ```
3. Loading the method<br>
    write the method
    ```
    List<string[]> list = ReadingFile.GetCSV("csv file path");
    ```
- If you specify a delimiter or character code<br>
    ```
    List<string[]> list = ReadingFile.GetCSV("csv file path", ',', "utf-8");
    ```

## Lisence
This project is licensed under the MIT License, see the LICENSE.txt file for details.