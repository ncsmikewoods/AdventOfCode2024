﻿using System;

namespace Day01;

class Program
{
    static void Main(string[] args)
    {
        var solver = new Solver();

        Part1(solver);
        Part2(solver);
    }

    static void Part1(Solver solver)
    {
        Console.WriteLine("Solving Part 1...");
        var start = DateTime.Now;

        var result = solver.Solve1();
        var duration = DateTime.Now - start;

        Console.WriteLine($"Solution 1: {result}");
        Console.WriteLine($"Duration: {Math.Round(duration.TotalMilliseconds)}ms");
        Console.WriteLine("");
    }

    static void Part2(Solver solver)
    {
        Console.WriteLine("Solving Part 2...");
        var start = DateTime.Now;
        
        var result = solver.Solve2();
        var duration = DateTime.Now - start;
        
        Console.WriteLine($"Solution 2: {result}");
        Console.WriteLine($"Duration: {Math.Round(duration.TotalMilliseconds)}ms");
        Console.WriteLine("");
    }
}