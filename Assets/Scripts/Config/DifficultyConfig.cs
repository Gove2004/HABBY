

// 定义难度配置
using System.Collections.Generic;

public static class DifficultyConfig
{
    public static readonly List<List<int>> difficultySettings = new List<List<int>>()
    {
        new List<int>() { 1, 0, 0, 0, 0, 0},
        new List<int>() { 2, 0, 0, 0, 0, 0},
        new List<int>() { 3, 0, 0, 0, 0, 0},
        new List<int>() { 5, 0, 0, 0, 0, 0},
        new List<int>() { 7, 0, 0, 0, 0, 0},
        new List<int>() { 10, 0, 0, 1, 0, 0},  // Boss 1

        new List<int>() { 5, 1, 0, 0, 0, 0},
        new List<int>() { 5, 3, 0, 0, 0, 0},
        new List<int>() { 5, 5, 0, 0, 0, 0},
        new List<int>() { 10, 5, 0, 0, 0, 0},
        new List<int>() { 15, 5, 0, 0, 0, 0},
        new List<int>() { 15, 5, 0, 0, 1, 0},  // Boss 2
        
        new List<int>() { 10, 5, 1, 0, 0, 0},
        new List<int>() { 12, 7, 3, 0, 0, 0},
        new List<int>() { 15, 10, 5, 0, 0, 0},
        new List<int>() { 20, 10, 7, 0, 0, 0},
        new List<int>() { 25, 15, 10, 0, 0, 0},
        new List<int>() { 25, 15, 10, 0, 0, 1},  // Boss 3
    
        new List<int>() { 30, 15, 10, 0, 0, 0},
        new List<int>() { 40, 20, 12, 0, 0, 0},
        new List<int>() { 50, 25, 15, 0, 0, 0},
        new List<int>() { 75, 30, 20, 0, 0, 0},
        new List<int>() { 100, 40, 25, 0, 0, 0},
        new List<int>() { 100, 50, 25, 1, 1, 1},  // Boss ...
    };  

}