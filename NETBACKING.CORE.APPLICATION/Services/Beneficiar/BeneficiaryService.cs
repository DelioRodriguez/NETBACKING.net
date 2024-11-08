using AutoMapper;
using NETBACKING.CORE.APPLICATION.Exceptions;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Beneficiarys;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Beneficiarys;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Beneficiary;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.Beneficiar;

public class BeneficiaryService : Service<Beneficiary>, IBeneficiaryService
{
    private readonly IMapper _mapper;
    private readonly IBeneficiaryRepository _beneficiaryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    
    public BeneficiaryService(IRepository<Beneficiary> repository, IMapper mapper, IBeneficiaryRepository repository1, IProductRepository productRepository, IUserRepository userRepository) : base(repository)
    {
        _mapper = mapper;
        _beneficiaryRepository = repository1;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }
    
    public async Task<List<BeneficiaryViewModel>> GetByIdUserAsyncModel(string? id)
    {
        return _mapper.Map<List<BeneficiaryViewModel>>(await _beneficiaryRepository.GetByIdUserAsyncModel(id));
    }

    public async Task AddAsyncByModel(string idCuenta)
    {
        if (await _beneficiaryRepository.BeneficiaryExistsAsync(idCuenta))
        {
            throw new AddBeneficiaryException("Ya existe un beneficiario con ese número de cuenta.", null);
        }
        
        try
        {
            var product = await _productRepository.GetProductByIdentificador(idCuenta);

            if (product != null)
            {
                var user = await _userRepository.GetUserByIdAsync(product.ApplicationUserId);

                if (user == null)
                {
                    throw new AddBeneficiaryException("El usuario asociado al producto no fue encontrado.", null);
                }
                
                    var beneficiary = new Beneficiary
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ApplicationUserId = user.Id,
                        AccountNumber = product.UniqueIdentifier
                    };
                
                    await _beneficiaryRepository.AddAsync(beneficiary);
                
            }
        }
        catch (Exception e)
        {
            throw new AddBeneficiaryException("Error al intentar agregar un beneficiario.", e);
        }
    }
    
}