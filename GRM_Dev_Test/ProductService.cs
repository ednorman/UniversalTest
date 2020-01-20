namespace GRM_Dev_Test
{
    public abstract class ProductService
    {
        public abstract string Validate(string args);
        public abstract void LoadData();

        public string TemplateMethod(string args)
        {
            LoadData();
            return Validate(args);
        }
    }
}
