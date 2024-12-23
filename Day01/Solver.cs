﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

namespace Day01;

public class Solver
{
    private List<int> _left;
    private List<int> _right;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        _left = _left.Order().ToList();
        _right = _right.Order().ToList();

        var combined = _left.Zip(_right);

        var sum = combined.Sum(pair => Math.Abs(pair.First - pair.Second));
        return sum;
    }

    public int Solve2()
    {
        var valueMap = GetValueMap(_right);

        var similarityScore = _left.Sum(x =>
        {
            valueMap.TryGetValue(x, out var multiplier);
            return x * multiplier;
        });
        return similarityScore;
    }

    Dictionary<int, int> GetValueMap(List<int> values)
    {
        var map = new Dictionary<int, int>();

        foreach (var val in values)
        {
            if (!map.ContainsKey(val))
            {
                map[val] = 1;
                continue;
            }
            map[val]++;
        }

        return map;
    }

    void GetInputs()
    {
        var lines = File.ReadAllLines("input.txt");

        _left = new List<int>();
        _right = new List<int>();
        
        foreach (var line in lines)
        {
            var tokens = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            _left.Add(int.Parse(tokens[0]));
            _right.Add(int.Parse(tokens[1]));
        }
    }
}