using SkrinTestTask.Repositories.Interfaces;


namespace SkrinTestTask.Services
{
    public class XmlFileReader : IFileReader
    {
        private IUserRepository userRepository;
        private IOrderRepository orderRepository;
        private IProductRepository productRepository;
        private IOrderItemRepository orderItemRepository;
        public string Extention { get; } = ".xml";

        public XmlFileReader(IUserRepository userRepository, IOrderRepository orderRepository, 
            IProductRepository productRepository, IOrderItemRepository orderItemRepository) 
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public void ReadFromFile (string filePath)
        {
            var fileExtention = Path.GetExtension(filePath);
            if (fileExtention.Equals(Extention, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Expected {Extention} file extention, got {fileExtention} instead. {filePath}");
        }
    }
}
