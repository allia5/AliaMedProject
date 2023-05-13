using DTO;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.Components.Forms;
using Server.Models.CabinetMedicals;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;

namespace Server.Services.Foundation.CabinetMedicalService
{
    public partial class CabinetMedicalService
    {
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateEntryString(string Entry)
        {
            if (String.IsNullOrWhiteSpace(Entry))
            {
                throw new NullException(nameof(Entry));
            }
        }
        public void ValidateCabinetMedicalIsNull(CabinetMedical cabinetMedical)
        {
            if (cabinetMedical == null)
            {
                throw new NullDataStorageException(nameof(cabinetMedical));
            }
        }
        public void ValidateCabinetMedicalDtoIsNull(CabinetMedicalDto cabinetMedicalDto)
        {
            if (cabinetMedicalDto == null)
            {
                throw new NullException(nameof(cabinetMedicalDto));
            }
        }
      
        public bool ValidateImage(byte[] image)
        {
            Dictionary<string, string> fileSignatures = new Dictionary<string, string>
    {
         { ".jpg", "FFD8FF" },
    { ".png", "89504E47" },
    { ".gif", "47494638" },
    { ".bmp", "424D" },

    };

            // Get the file signature from the byte array
            string fileSignature = BitConverter.ToString(image.Take(4).ToArray()).Replace("-", "");

            // Determine the file type based on the file signature
            foreach (KeyValuePair<string, string> entry in fileSignatures)
            {
                if (fileSignature.StartsWith(entry.Value))
                {
                    return true;
                }
            }
            return false;
          
        }
        public void ValidateEntryOnUpdate(string Email, CabinetMedicalDto cabinetMedicalDto)
        {
            if(cabinetMedicalDto.Image != null)
            {
               if(!ValidateImage(cabinetMedicalDto.Image))
                {
                    throw new ArgumentNullException();
                }
            }
            ValidateEntryString(Email);
            ValidateCabinetMedicalDtoIsNull(cabinetMedicalDto);
            ValidateEntryString(cabinetMedicalDto.phoneNumber);
            ValidateEntryString(cabinetMedicalDto.name);
            ValidateEntryString(cabinetMedicalDto.Services);
            ValidateEntryString(cabinetMedicalDto.Adress);
            ValidateEntryString(cabinetMedicalDto.Status.ToString());
            ValidateEntryString(cabinetMedicalDto.JobTime);



        }
    }
}
