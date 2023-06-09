﻿using DTO;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;

namespace Server.Services.Foundation.FileMedicalService
{
    public partial class FileMedicalService
    {
        public delegate Task<FileMedicalPatientDto> ReturningFileMedicalPatientAsync();
        public delegate Task<FileMedicalMainPatientDto> ReturningFileMedicalMainPatientAsync();
        public delegate Task ReturningUpdateFileMedical();
       
        public delegate Task<List<FileMedicalPatientDto>> ReturningListFileMedicalPatientAsync();   

        public async Task<List<FileMedicalPatientDto>> TryCatch_(ReturningListFileMedicalPatientAsync returningListFileMedical)
        {
            try
            {
                return await returningListFileMedical();
            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

     

        public async Task TryCatch(ReturningUpdateFileMedical returningUpdateFileMedical)
        {
            try
            {
                await returningUpdateFileMedical();
            }
            catch (ArgumentNullException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch (NullException Ex)
            {
                throw new ServiceException(Ex);
            }catch(StatusValidationException Ex)
            {
                throw new ValidationException(Ex);
            }
            catch(CompatibilityError Ex)
            {
                throw new ValidationException(Ex);
            }
        }
        public async Task<FileMedicalMainPatientDto> TryCatch(ReturningFileMedicalMainPatientAsync returningFileMedicalMainPatient)
        {
            try
            {
                return await returningFileMedicalMainPatient();
            }catch (NullException ex)
            {
                throw new ServiceException(ex);
            }
            catch (ArgumentNullException ex )
            {
                throw new ValidationException(ex);
            }
            catch (CompatibilityError ex)
            {
                throw new ValidationException(ex);
            }
            catch (StatusValidationException ex)
            {
                throw new ValidationException(ex);
            }
        }
        public async Task<FileMedicalPatientDto> TryCatch(ReturningFileMedicalPatientAsync returningFileMedicalPatient)

        {
            try
            {
                return await returningFileMedicalPatient();
            }catch (NullException ex)
            {
                throw new ValidationException(ex.InnerException);
            }
            catch (CompatibilityError Ex)
            {
                throw new ServiceException(Ex.InnerException);
            }catch(StatusValidationException ex)
            {
                throw new ServiceException(ex.InnerException);
            }catch(ArgumentException ex)
            {
                throw new ServiceException(ex.InnerException);
            }
        }
    }
}
