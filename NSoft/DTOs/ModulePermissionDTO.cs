namespace NSoft.DTOs
{
    public class ModulePermissionDTO
    {
        public int ModuloId { get; set; }
        public string ModuloCodigo { get; set; }    
        public string NombreModulo { get; set; }
        public string Descripcion { get; set; }
        public List<int> Permissions { get; set; }
    }
    public class ListModulePermissionResponseDTO
    {
        public List<ModulePermissionDTO> Modules  { get; set; }   
    }

}
