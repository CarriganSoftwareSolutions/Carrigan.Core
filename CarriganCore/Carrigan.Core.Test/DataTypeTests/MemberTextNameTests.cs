using Carrigan.Core.DataTypes;
using Carrigan.Core.Extensions;
using Carrigan.Core.Test.DataTypeTests.Examples;

//IGNORE SPELLING: abc
//This is actually testing the base class.
namespace Carrigan.Core.Test.DataTypeTests;
public class MemberTextNameTests
{
    private readonly string white = " ";
    private readonly string? nul = null;
    private readonly string empty = string.Empty;
    private readonly decimal pi = 3.14159m;
    private readonly decimal t = 6.28318m;
    private readonly decimal e = 2.71828m;
    private readonly decimal golden = 1.618m;
    private readonly decimal zero = 0m;
    private readonly string eStr = "E is 2.71828";
    private readonly string eStrAlt = "E is 2.71828";
    private readonly string goldenString = "Golden Ratio is 1.618";
    private readonly string tString = "t is 6.28318m";
    private readonly string piString = "Pi is 3.14159m";
    [Fact]
    public void Constructor_Null()
    {
        MemberName nameWrapper = new (nul);
        Assert.NotNull(nameWrapper);
    }

    [Fact]
    public void New_Null()
    {
        MemberTextName? nameWrapper = MemberTextName.New(nul);
        Assert.Null(nameWrapper);
    }

    [Fact]
    public void New_Empty()
    {
        MemberTextName? nameWrapper = MemberTextName.New(empty);
        Assert.Null(nameWrapper);
    }

    [Fact]
    public void New_White()
    {
        MemberTextName? nameWrapper = MemberTextName.New(white);
        Assert.NotNull(nameWrapper);
    }


    [Fact]
    public void ToString_Null()
    {
        MemberTextName nameWrapper = new(nul);
        Assert.Equal(string.Empty, nameWrapper.ToString());
    }

    [Fact]
    public void ToString_Empty()
    {
        MemberTextName nameWrapper = new(empty);
        Assert.Equal(empty, nameWrapper.ToString());
    }

    [Fact]
    public void ToString_Text()
    {
        MemberTextName nameWrapper = new(eStr);
        Assert.Equal(eStr, nameWrapper.ToString());
    }

    [Fact]
    public void Equal_Null()
    {
        string? name = null;

        MemberTextName nameWrapper1 = new(name);
        MemberTextName nameWrapper2 = new(name);
        Assert.Equal(nameWrapper1, nameWrapper2);
        Assert.Equal(string.Empty, nameWrapper2.ToString());
    }

    [Fact]
    public void Equal_Empty()
    {
        string? name = string.Empty;

        MemberTextName nameWrapper1 = new(name);
        MemberTextName nameWrapper2 = new(name);
        Assert.Equal(nameWrapper1, nameWrapper2);
        Assert.Equal(string.Empty, nameWrapper2.ToString());
    }

    [Fact]
    public void Equal_Default_Text()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(eStrAlt);
        Assert.Equal(nameWrapper1, nameWrapper2);
        Assert.Equal(eStr, nameWrapper2.ToString());
    }

    [Fact]
    public void EqualEqual_Null()
    {
        MemberTextName nameWrapper1 = new(nul);
        MemberTextName nameWrapper2 = new(nul);
        Assert.True(nameWrapper1 == nameWrapper2);
        Assert.True(string.Empty == nameWrapper2.ToString());
    }

    [Fact]
    public void EqualEqual_Empty()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(empty);
        Assert.True(nameWrapper1 == nameWrapper2);
        Assert.True(string.Empty == nameWrapper2.ToString());
    }

    [Fact]
    public void EqualEqual_Text()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(eStrAlt);
        bool test = eStr == nameWrapper2.ToString();
        bool test2 = eStr != nameWrapper2.ToString();
        Assert.True(nameWrapper1 == nameWrapper2);
        Assert.True(test);
        Assert.False(nameWrapper1 != nameWrapper2);
        Assert.False(test2);
    }

    [Fact]
    public void EqualEqual_NullComparisons()
    {
        MemberTextName nonNull = new(string.Empty);
        MemberTextName? isNull = null;

        Assert.False(nonNull == isNull);
        Assert.False(isNull == nonNull);
    }

    [Fact]
    public void NotEqualEqual_NullComparisons()
    {
        MemberTextName nonNull = new(string.Empty);
        MemberTextName? isNull = null;
        Assert.True(nonNull != isNull);
        Assert.True(isNull != nonNull);
    }

    [Fact]
    public void DictionaryKey_Null()
    {
        string? name = null;
        Dictionary<MemberTextName, decimal> dictionary= [];

        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(name);
        dictionary[nameWrapper1] = pi;
        Assert.True(dictionary.ContainsKey(nameWrapper2));
        Assert.True(dictionary[nameWrapper1] == pi);
        Assert.True(dictionary[nameWrapper2] == pi);
    }

    [Fact]
    public void DictionaryKey_Empty()
    {
        Dictionary<MemberTextName, decimal> dictionary = [];

        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(empty);
        dictionary[nameWrapper1] = pi;
        Assert.True(dictionary.ContainsKey(nameWrapper2));
        Assert.True(dictionary[nameWrapper1] == pi);
        Assert.True(dictionary[nameWrapper2] == pi);
    }

    [Fact]
    public void DictionaryKey_Text()
    {
        string? name = eStr;
        Dictionary<MemberTextName, decimal> dictionary = [];

        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(eStrAlt);
        dictionary[nameWrapper1] = pi;
        Assert.True(dictionary.ContainsKey(nameWrapper2));
        Assert.True(dictionary[nameWrapper1] == pi);
        Assert.True(dictionary[nameWrapper2] == pi);
    }

    [Fact]
    public void DictionaryKey_Multi()
    {
        string? name = eStr;
        Dictionary<MemberTextName, decimal> dictionary = [];

        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(eStrAlt);
        MemberTextName nameWrapper3 = new(tString);
        MemberTextName nameWrapper4 = new(goldenString);
        MemberTextName nameWrapper5 = new(piString);
        MemberTextName nameWrapper6 = new(empty);
        dictionary[nameWrapper1] = e;
        dictionary[nameWrapper3] = t;
        dictionary[nameWrapper4] = pi;
        dictionary[nameWrapper5] = golden;
        dictionary[nameWrapper6] = zero;
        Assert.True(dictionary.ContainsKey(nameWrapper2));
        Assert.True(dictionary[nameWrapper1] == e);
        Assert.True(dictionary[nameWrapper2] == e);
        Assert.True(dictionary[nameWrapper3] == t);
        Assert.True(dictionary[nameWrapper4] == pi);
        Assert.True(dictionary[nameWrapper5] == golden);
        Assert.True(dictionary[nameWrapper6] == zero);
        Assert.Equal(5, dictionary.Count);
    }



    [Fact]
    public void NotEqual_Null()
    {
        MemberTextName nameWrapper1 = new(nul);
        MemberTextName nameWrapper2 = new(goldenString);
        Assert.NotEqual(nameWrapper1, nameWrapper2);
        Assert.NotEqual(empty, nameWrapper2.ToString());
    }

    [Fact]
    public void NotEqual_Empty()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(goldenString);
        Assert.NotEqual(nameWrapper1, nameWrapper2);
        Assert.NotEqual(empty, nameWrapper2.ToString());
    }

    [Fact]
    public void NotEqual_Text()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(goldenString);
        Assert.NotEqual(nameWrapper1, nameWrapper2);
        Assert.NotEqual(eStr, nameWrapper2.ToString());
    }

    [Fact]
    public void NotEqualEqual_Null()
    {
        MemberTextName nameWrapper1 = new(nul);
        MemberTextName nameWrapper2 = new(eStr);
        Assert.True(nameWrapper1 != nameWrapper2);
        Assert.True(empty != nameWrapper2.ToString());
    }

    [Fact]
    public void NotEqualEqual_Empty()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(eStr);
        Assert.True(nameWrapper1 != nameWrapper2);
        Assert.True(empty != nameWrapper2.ToString());
    }

    [Fact]
    public void NotEqualEqual_Text()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(goldenString);
        Assert.True(nameWrapper1 != nameWrapper2);
        Assert.True(nameWrapper1 != nameWrapper2);
    }

    [Fact]
    public void IsEmpty_True()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        Assert.True(nameWrapper1.IsEmpty());
        Assert.True(nameWrapper2.IsEmpty());
    }
    [Fact]
    public void IsNullOrEmpty_True()
    {
        MemberTextName? nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        MemberTextName? nameWrapper3 = null;
        MemberTextName? nameWrapper4 = MemberTextName.New(nul);
        MemberTextName? nameWrapper5 = MemberTextName.New(empty);
        Assert.True(nameWrapper1.IsNullOrEmpty());
        Assert.True(nameWrapper2.IsNullOrEmpty());
        Assert.True(nameWrapper3.IsNullOrEmpty());
        Assert.True(nameWrapper4.IsNullOrEmpty());
        Assert.True(nameWrapper5.IsNullOrEmpty());
    }

    [Fact]
    public void IsEmpty_False()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(white);
        Assert.False(nameWrapper1.IsEmpty());
        Assert.False(nameWrapper2.IsEmpty());
    }

    [Fact]
    public void IsNullOrEmpty_False()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(white);
        MemberTextName? nameWrapper3 = MemberTextName.New(eStr);
        MemberTextName? nameWrapper4 = MemberTextName.New(white);
        Assert.False(nameWrapper1.IsNullOrEmpty());
        Assert.False(nameWrapper2.IsNullOrEmpty());
        Assert.False(nameWrapper3.IsNullOrEmpty());
        Assert.False(nameWrapper4.IsNullOrEmpty());
    }

    [Fact]
    public void IsNotEmpty_False()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        Assert.False(nameWrapper1.IsNotEmpty());
        Assert.False(nameWrapper2.IsNotEmpty());
    }

    [Fact]
    public void IsNotNullOrEmpty_False()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        MemberTextName? nameWrapper3 = null;
        MemberTextName? nameWrapper4 = MemberTextName.New(null);
        MemberTextName? nameWrapper5 = MemberTextName.New(nul);
        MemberTextName? nameWrapper6 = MemberTextName.New(empty);
        Assert.False(nameWrapper1.IsNotNullOrEmpty());
        Assert.False(nameWrapper2.IsNotNullOrEmpty());
        Assert.False(nameWrapper3.IsNotNullOrEmpty());
        Assert.False(nameWrapper4.IsNotNullOrEmpty());
        Assert.False(nameWrapper5.IsNotNullOrEmpty());
        Assert.False(nameWrapper6.IsNotNullOrEmpty());
    }

    [Fact]
    public void IsNotEmpty_True()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(white);
        Assert.True(nameWrapper1.IsNotEmpty());
        Assert.True(nameWrapper2.IsNotEmpty());
    }

    [Fact]
    public void IsNotNullOrEmpty_True()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(white);
        MemberTextName? nameWrapper3 = MemberTextName.New(eStr);
        MemberTextName? nameWrapper4 = MemberTextName.New(white);
        Assert.True(nameWrapper1.IsNotNullOrEmpty());
        Assert.True(nameWrapper2.IsNotNullOrEmpty());
        Assert.True(nameWrapper3.IsNotNullOrEmpty());
        Assert.True(nameWrapper4.IsNotNullOrEmpty());
    }

    [Fact]
    public void IsWhitespace_True()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        MemberTextName nameWrapper3 = new(white);
        Assert.True(nameWrapper1.IsWhiteSpace());
        Assert.True(nameWrapper2.IsWhiteSpace());
        Assert.True(nameWrapper3.IsWhiteSpace());
    }

    [Fact]
    public void IsNullOrWhitespace_True()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        MemberTextName nameWrapper3 = new(white);
        MemberTextName? nameWrapper4 = null;
        MemberTextName? nameWrapper5 = MemberTextName.New(null);
        MemberTextName? nameWrapper6 = MemberTextName.New(nul);
        MemberTextName? nameWrapper7 = MemberTextName.New(empty);
        MemberTextName? nameWrapper8 = MemberTextName.New(white);
        Assert.True(nameWrapper1.IsNullOrWhiteSpace());
        Assert.True(nameWrapper2.IsNullOrWhiteSpace());
        Assert.True(nameWrapper3.IsNullOrWhiteSpace());
        Assert.True(nameWrapper4.IsNullOrWhiteSpace());
        Assert.True(nameWrapper5.IsNullOrWhiteSpace());
        Assert.True(nameWrapper6.IsNullOrWhiteSpace());
        Assert.True(nameWrapper7.IsNullOrWhiteSpace());
        Assert.True(nameWrapper8.IsNullOrWhiteSpace());
    }

    [Fact]
    public void IsWhitespace_False()
    {
        MemberTextName nameWrapper1 = new(eStr);
        Assert.False(nameWrapper1.IsWhiteSpace());
    }

    [Fact]
    public void IsNullOrWhitespace_False()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName? nameWrapper2 = MemberTextName.New(eStr);
        Assert.False(nameWrapper1.IsNullOrWhiteSpace());
        Assert.False(nameWrapper2.IsNullOrWhiteSpace());
    }

    [Fact]
    public void IsNotWhitespace_False()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        MemberTextName nameWrapper3 = new(white);
        Assert.False(nameWrapper1.IsNotWhiteSpace());
        Assert.False(nameWrapper2.IsNotWhiteSpace());
        Assert.False(nameWrapper3.IsNotWhiteSpace());
    }

    [Fact]
    public void IsNotNullOrWhitespace_False()
    {
        MemberTextName nameWrapper1 = new(empty);
        MemberTextName nameWrapper2 = new(nul);
        MemberTextName nameWrapper3 = new(white);
        MemberTextName? nameWrapper4 = null;
        MemberTextName? nameWrapper5 = MemberTextName.New(empty);
        MemberTextName? nameWrapper6 = MemberTextName.New(nul);
        MemberTextName? nameWrapper7 = MemberTextName.New(white);
        Assert.False(nameWrapper1.IsNotNullOrWhiteSpace());
        Assert.False(nameWrapper2.IsNotNullOrWhiteSpace());
        Assert.False(nameWrapper3.IsNotNullOrWhiteSpace());
        Assert.False(nameWrapper4.IsNotNullOrWhiteSpace());
        Assert.False(nameWrapper5.IsNotNullOrWhiteSpace());
        Assert.False(nameWrapper6.IsNotNullOrWhiteSpace());
        Assert.False(nameWrapper7.IsNotNullOrWhiteSpace());
    }

    [Fact]
    public void IsNotWhitespace_True()
    {
        MemberTextName nameWrapper1 = new(eStr);
        Assert.True(nameWrapper1.IsNotWhiteSpace());
    }

    [Fact]
    public void IsNotNullOrWhitespace_True()
    {
        MemberTextName nameWrapper1 = new(eStr);
        Assert.True(nameWrapper1.IsNotNullOrWhiteSpace());
    }

    [Fact]
    public void ExplicitConversion_ToString_AssignmentAndInterpolation()
    {
        MemberTextName a = new(eStr);
        string assigned = a.ToString();
        string interpolated = $"{a}";

        Assert.Equal(eStr, assigned);
        Assert.Equal(eStr, interpolated);
    }

    [Fact]
    public void GetHashCode_EqualObjects_SameHash()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new(eStr);
        Assert.Equal(nameWrapper1, nameWrapper2);
        Assert.Equal(nameWrapper1.GetHashCode(), nameWrapper2.GetHashCode());
    }

    [Fact]
    public void Equality_PreservesWhitespace()
    {
        MemberTextName nameWrapper1 = new(eStr);
        MemberTextName nameWrapper2 = new($" {eStr} ");
        Assert.NotEqual(nameWrapper1, nameWrapper2);
        Assert.True(nameWrapper1 != nameWrapper2);
        Assert.False(nameWrapper2.IsWhiteSpace()); 
    }

    [Fact]
    public void Equals_ObjectAndTyped_Agree()
    {
        MemberTextName nameWrapper1 = new(eStr);
        object nameWrapper2 = new MemberTextName(eStr);
        Assert.True(nameWrapper1.Equals((TextWrapper)nameWrapper2));
        Assert.True(nameWrapper1.Equals(nameWrapper2));
    }

    [Fact]
    public void Equals_DifferentStringComparison()
    {
        MemberTextName left = new("abc");
        MemberTextNameIgnoreCase right = new("ABC");

        Assert.Throws<InvalidOperationException>(() => left.Equals((TextWrapper)right));
        Assert.Throws<InvalidOperationException>(() => right.Equals((TextWrapper)left));
    }

    [Fact]
    public void CompareTo_DifferentStringComparison()
    {
        MemberTextName left = new("abc");
        MemberTextNameIgnoreCase right = new("ABC");

        Assert.Throws<InvalidOperationException>(() => left.CompareTo(right));
        Assert.Throws<InvalidOperationException>(() => right.CompareTo(left));
    }

    [Fact]
    public void EqualityComparer_Equals_DifferentStringComparisonFromComparer()
    {
        MemberTextName comparer = new("x");
        MemberTextNameIgnoreCase x = new("abc");
        MemberTextNameIgnoreCase y = new("ABC");

        Assert.Throws<InvalidOperationException>(() => comparer.Equals(x, y));
    }

    [Fact]
    public void EqualityComparer_GetHashCode_DifferentStringComparisonFromComparer()
    {
        MemberTextName comparer = new("x");
        MemberTextNameIgnoreCase obj = new("abc");

        Assert.Throws<InvalidOperationException>(() => comparer.GetHashCode((TextWrapper)obj));
    }
    [Fact]
    public void Equals_DifferentStringComparison_Throws()
    {
        MemberTextName left = new("abc");
        MemberTextNameIgnoreCase right = new("ABC");

        Assert.Throws<InvalidOperationException>(() => left.Equals((TextWrapper)right));
        Assert.Throws<InvalidOperationException>(() => right.Equals((TextWrapper)left));
    }

    [Fact]
    public void CompareTo_DifferentStringComparison_Throws()
    {
        MemberTextName left = new("abc");
        MemberTextNameIgnoreCase right = new("ABC");

        Assert.Throws<InvalidOperationException>(() => left.CompareTo(right));
        Assert.Throws<InvalidOperationException>(() => right.CompareTo(left));
    }

    [Fact]
    public void EqualityComparer_Equals_DifferentStringComparisonFromComparer_Throws()
    {
        MemberTextName comparer = new("x");
        MemberTextNameIgnoreCase x = new("abc");
        MemberTextNameIgnoreCase y = new("ABC");

        Assert.Throws<InvalidOperationException>(() => comparer.Equals(x, y));
    }

    [Fact]
    public void EqualityComparer_GetHashCode_DifferentStringComparisonFromComparer_Throws()
    {
        MemberTextName comparer = new("x");
        MemberTextNameIgnoreCase obj = new("abc");

        Assert.Throws<InvalidOperationException>(() => comparer.GetHashCode((TextWrapper)obj));
    }
}
