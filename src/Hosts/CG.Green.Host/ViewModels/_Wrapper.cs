namespace CG.Green.Host.ViewModels;

/// <summary>
/// This class wraps a string for the <see cref="MudTable{T}"/> component, since 
/// that type is incapable of binding (correctly) to a single string object, as 
/// documented HERE: https://github.com/MudBlazor/MudBlazor/discussions/6217
/// </summary>
public class _Wrapper
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the string value.
    /// </summary>
    public string Value { get; set; } = null!;

    #endregion
}
