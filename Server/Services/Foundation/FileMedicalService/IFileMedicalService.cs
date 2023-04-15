﻿using DTO;

namespace Server.Services.Foundation.FileMedicalService
{
    public interface IFileMedicalService
    {
        public Task<FileMedicalPatientDto> AddNewFileMedicalPatient(string Email, FileMedicalToAddDto fileMedicalToAdd);
    }
}