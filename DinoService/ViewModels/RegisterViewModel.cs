﻿using System.ComponentModel.DataAnnotations;

namespace DinoService.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "ConfirmPassword is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}
