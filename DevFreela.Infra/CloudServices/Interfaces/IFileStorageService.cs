namespace DevFreela.Infra.CloudServices.Interfaces
{
    public interface IFileStorageService
    {
        void UploadFile(byte[] data, string fileName);
    }
}
