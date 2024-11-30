using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;
using Core.Specification;

namespace Core.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}
