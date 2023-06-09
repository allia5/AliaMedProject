﻿using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.RadioMedicalService
{
    public partial class RadioMedicalService
    {
        public delegate Task<InformationRadioResultDto> GetInformationRadioResultDtoDelegate();
        public delegate Task<byte[]> ReturningByteFileMedical();

        public async Task<InformationRadioResultDto> TryCatch(GetInformationRadioResultDtoDelegate informationRadioResultDto)
        {
            try
            {
                return await informationRadioResultDto();
            }
            catch (ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (NullDataStorageException Ex)
            {
                throw new  StorageValidationException(Ex);
            }
            catch (FormatException Ex)
            {
                throw new ValidationException(Ex);
            }
        }
        public async Task<byte[]> TryCatch(ReturningByteFileMedical returningByteFileMedical)
        {
            try
            {
                return await returningByteFileMedical();
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationException(ex);
            }
            catch (NullException ex)
            {
                throw new ServiceException(ex);
            }
        }
    }
}
