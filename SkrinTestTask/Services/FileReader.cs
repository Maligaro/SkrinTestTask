using SkrinTestTask.Repositories.Interfaces;


namespace SkrinTestTask.Services
{
    public class XlsFileReader : IFileReader
    {
        private IUserRepository userRepository;
        private IOrderRepository orderRepository;
        private IProductRepository productRepository;
        private IOrderItemRepository orderItemRepository;

        public XlsFileReader(IUserRepository userRepository, IOrderRepository orderRepository, 
            IProductRepository productRepository, IOrderItemRepository orderItemRepository) 
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public void ReadFromFile (string filePath)
        {

        }
    }
}
