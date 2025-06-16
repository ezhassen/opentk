namespace OpenTK.Tests

open Xunit
open OpenTK.Mathematics

module Half =
    [<Fact>]
    let ``Casting Half to Single and back to Half is lossless`` () =
        for bits = int System.Int16.MinValue to int System.Int16.MaxValue do
            let bytes = System.BitConverter.GetBytes(int16 bits)
            let half = TKHalf.FromBytes(bytes, 0)
            let single = float32 half
            let roundtrip = TKHalf single

            Assert.True((half.IsNaN && roundtrip.IsNaN) || half = roundtrip)

    [<Fact>]
    let ``Half.ToString and Single.ToString return same string for same value`` () =
        for bits = int System.Int16.MinValue to int System.Int16.MaxValue do
            let bytes = System.BitConverter.GetBytes(int16 bits)
            let half = TKHalf.FromBytes(bytes, 0)
            let single = float32 half
            Assert.Equal(half.ToString(), single.ToString())

    [<Fact>]
    let ``Half can represent all integers from -2048 to 2048 exactly`` () =
        for i = -2048 to 2048 do
            let single = float32 i
            let half = TKHalf single
            Assert.Equal(single, float32 half)

    [<Fact>]
    let ``Single NaN and inifnity can be cast to Half NaN and infinity`` () =
        Assert.True((TKHalf System.Single.NaN).IsNaN)
        Assert.True((TKHalf System.Single.PositiveInfinity).IsPositiveInfinity)
        Assert.True((TKHalf System.Single.NegativeInfinity).IsNegativeInfinity)
