﻿using DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using System.Drawing;

namespace Server.Services.UserService
{
    public partial class UserService
    {
        public static bool ArePropertiesNull(object obj)
        {
            if (obj == null)
                return true;

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.GetValue(obj) == null)
                    return true;
            }

            return false;
        }
        public void ValidateEntryResetPassword(ResetPasswordUserAccountDto resetPasswordUserAccountDto)
        {
            if(resetPasswordUserAccountDto.Password != resetPasswordUserAccountDto.ConfirmePassword)
            {
                throw new InvalidException(nameof(resetPasswordUserAccountDto.Password), resetPasswordUserAccountDto.Password,"User");
            }
            if (ArePropertiesNull(resetPasswordUserAccountDto))
            {
                throw new ArgumentNullException();
            }
        }
        public void ValidateIdentityResult(IdentityResult identityResult,string Token)
        {
            if (!identityResult.Succeeded)
            {
                throw new InvalidException("Token",Token, "User");
            }
        }
        public void ValidateOnForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email)) {  throw new ArgumentNullException(nameof(Email)); }
        }
        public async Task ValidateUsenOnCreate(RegistreAccountDto registreAccountDto)
        {
            await ValidateUserAccountIsNotInSystem(registreAccountDto.Email);
            ValidateRegistreUserIsNull(registreAccountDto);
            ValidateUserFields(registreAccountDto);
            ValidateInvalidAuditFields(registreAccountDto);


        }
        public void ValidateEntryOnLogin(LoginAccountDto loginAccountDto)
        {
            if (loginAccountDto != null)
            {
                if (IsInvalid(loginAccountDto.Email) == true)
                {
                    throw new NullException(nameof(loginAccountDto.Email));
                }
                else if (IsInvalid(loginAccountDto.Password) == true)
                {
                    throw new NullException(nameof(loginAccountDto.Password));
                }
            }
            else
            {
                throw new NullException(nameof(loginAccountDto));
            }
        }
        public void ValidateAuthentificationPassword(bool Password)
        {
            if (Password != true)
            {
                throw new IdentityTokenException(nameof(Password));
            }
        }
        public void ValidateListUserRolesIsNull(IQueryable UserRoles)
        {
            var list = UserRoles.Cast<UserRole>().ToList();
            if (list.Count() == 0)
            {
                throw new NullDataStorageException(nameof(UserRoles));
            }


        }
        public void ValidateEntryConfirmeEmail(string id, string Token)
        {
            if (IsInvalid(id) == true && IsInvalid(Token) == true)
            {
                throw new NullException(nameof(id) + nameof(Token));
            }
            else if (IsInvalid(id) == true)
            {
                throw new NullException(nameof(id));
            }
            else if (IsInvalid(Token) == true)
            {
                throw new NullException(nameof(Token));
            }

        }

        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateIdentityToken(IdentityResult identityResult)
        {
            if (identityResult.Succeeded == false)
            {
                throw new IdentityTokenException(nameof(identityResult));
            }
        }

        private void ValidateUserFields(RegistreAccountDto registreAccountDto)
        {
            switch (registreAccountDto)
            {
                case { } when IsInvalid(registreAccountDto.Password):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.Password),
                    parameterValue: registreAccountDto.Password, nameof(registreAccountDto));
                case { } when IsInvalid(registreAccountDto.Email):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.Email),
                    parameterValue: registreAccountDto.Email, nameof(registreAccountDto));
                case { } when IsInvalid(registreAccountDto.NationalNumber):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.NationalNumber),
                    parameterValue: registreAccountDto.NationalNumber, nameof(registreAccountDto));
                case { } when IsInvalid(registreAccountDto.PhoneNumber):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.PhoneNumber),
                    parameterValue: registreAccountDto.PhoneNumber, nameof(registreAccountDto));
                case { } when IsInvalid(registreAccountDto.ConfirmePassword):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.ConfirmePassword),
                    parameterValue: registreAccountDto.ConfirmePassword, nameof(registreAccountDto));





            }
        }



        private void ValidateInvalidAuditFields(RegistreAccountDto registreAccountDto)
        {
            switch (registreAccountDto)
            {
                case { } when !ChackNumberValidate(registreAccountDto.PhoneNumber):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.PhoneNumber),
                    parameterValue: registreAccountDto.PhoneNumber, nameof(registreAccountDto));
                /*case { } when !ChackNumberValidate(registreAccountDto.NationalNumber):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.NationalNumber),
                    parameterValue: registreAccountDto.NationalNumber, nameof(registreAccountDto));*/
                case { } when ChackUserSexe((int)registreAccountDto.Sexe):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.Sexe),
                    parameterValue: ((int)registreAccountDto.Sexe), nameof(registreAccountDto));
                case { } when ValidateDate(registreAccountDto.DateOfBirth):
                    throw new InvalidException(
                    parameterName: nameof(registreAccountDto.DateOfBirth),
                    parameterValue: registreAccountDto.DateOfBirth, nameof(registreAccountDto));

            }

        }
        private static void ValidateRegistreUserIsNull(RegistreAccountDto registreAccountDto)
        {
            if (registreAccountDto is null)
            {
                throw new NullException(nameof(registreAccountDto));
            }
        }


        public static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        public static bool ValidateDate(DateTime Date)
        {
            if (DateTime.UtcNow > Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ChackNumberValidate(string Field) { int n; var isNumeric = Int32.TryParse(Field, out n); return isNumeric; }
        public static bool ChackUserSexe(int Sexe) { if (Sexe != 1 || Sexe != 2) { return false; } return true; }
        public static bool CheckUserIsNull(User user)
        {
            if (user is null)
            {
                return true;
            }
            return false;
        }

        public void ValidationDoctorIsNull(Doctors doctor)
        {
            if (doctor == null)
            {
                throw new NullException(nameof(doctor));

            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullException(nameof(cabinetMedical));
            }
        }
        public void ValidateUserAccount(User UserAccount)
        {
            if (UserAccount.EmailConfirmed == false)
            { throw new NullException(nameof(UserAccount)); }
            else if(UserAccount.Status == UserStatus.Deactivated)
            {
                throw new NullException(nameof(UserAccount));
            }
        }

    }
}
