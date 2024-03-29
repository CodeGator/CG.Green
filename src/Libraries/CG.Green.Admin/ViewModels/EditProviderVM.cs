﻿
namespace CG.Green.ViewModels;

/// <summary>
/// This class is a view-model that represents a Duende identity provider.
/// </summary>
public class EditProviderVM
{
	// *******************************************************************
	// Properties.
	// *******************************************************************

	#region Properties

	/// <summary>
	/// This property contains the name of the identity provider.
	/// </summary>
	[Required]
	[Display(ShortName = "Name")]
	[MaxLength(Globals.Models.Clients.IdentityProviderNameLength)]
	public string Name { get; set; } = null!;

	#endregion
}
