using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using CyberSeedSetter.Cheats;
using HarmonyLib;
using static HarmonyLib.AccessTools;
using Random = UnityEngine.Random;

namespace CyberSeedSetter;
[HarmonyPatch(typeof(EndlessGrid))]
public static class EndlessGridPatcher
{
    // TODO: calculate a way to restore the inner state of the random object
    //       so it can be activated mid-run and return the same expected results without needing a restart
    // - calculate every random call that would've been made
    // - or create a new randomizer object that returns numbers based on the seed and the current state of the game
    //   (bad idea, risks indeterminism or "not true" randomness. A very narrow window to get it right)

    private static readonly MethodInfo m_Random_Range_Int = Method(typeof(Random), nameof(Random.Range), [typeof(int), typeof(int)]);
    private static readonly MethodInfo m_Random_Range_Float = Method(typeof(Random), nameof(Random.Range), [typeof(float), typeof(float)]);

    public static IEnumerable<CodeInstruction> Random_Range_Replacer(IEnumerable<CodeInstruction> instructions)
    {
        foreach (var inst in instructions)
        {
            if (inst.Calls(m_Random_Range_Int))
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                inst.operand = m_EndlessGridPatcher_Random_Range_Int;
            }
            else if (inst.Calls(m_Random_Range_Float))
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                inst.operand = m_EndlessGridPatcher_Random_Range_Float;
            }

            yield return inst;
        }
    }
    private static readonly MethodInfo m_EndlessGridPatcher_Random_Range_Int = ((Delegate)Random_Range_Int).Method;
    private static int Random_Range_Int(int min, int max, EndlessGrid endlessGrid)
    {
        return SeedOverrideCheat.RandomInstance.Next(min, max);
    }

    private static readonly MethodInfo m_EndlessGridPatcher_Random_Range_Float = ((Delegate)Random_Range_Float).Method;
    private static float Random_Range_Float(float min, float max, EndlessGrid endlessGrid)
    {
        var num = (float)SeedOverrideCheat.RandomInstance.NextDouble();
        return min + ((max - min) * num);
    }
}