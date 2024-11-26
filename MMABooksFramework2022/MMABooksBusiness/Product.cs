using System;

using MMABooksTools;
using MMABooksProps;
using MMABooksDB;

using System.Collections.Generic;

namespace MMABooksBusiness
{
    public class Product : BaseBusiness
    {
        public int ProductID
        {
            get => ((ProductProps)mProps).ProductID;
            set => ((ProductProps)mProps).ProductID = value;
        }

        public String ProductCode
        {
            get => ((ProductProps)mProps).ProductCode;
            set
            {
                if (!(value == ((ProductProps)mProps).ProductCode))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 10)
                    {
                        mRules.RuleBroken("ProductCode", false);
                        ((ProductProps)mProps).ProductCode = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("ProductCode cannot be more than 10 characters long.");
                    }
                }
            }
        }

        public String Description
        {
            get => ((ProductProps)mProps).Description;
            set
            {
                if (!(value == ((ProductProps)mProps).Description))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 50)
                    {
                        mRules.RuleBroken("Description", false);
                        ((ProductProps)mProps).Description = value;
                        mIsDirty = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Description must be no more than 50 characters long.");
                    }
                }
            }
        }

        public Decimal UnitPrice 
        { 
            get => ((ProductProps)mProps).UnitPrice; 
            set => ((ProductProps)mProps).UnitPrice = value;
        }

        public int OnHandQuantity 
        { 
            get => ((ProductProps)mProps).OnHandQuantity;
            set => ((ProductProps)mProps).OnHandQuantity = value;
        }

        public override object GetList()
        {
            List<Product> products = new List<Product>();
            List<ProductProps> props = new List<ProductProps>();

            props = (List<ProductProps>)mdbReadable.RetrieveAll();
            foreach (ProductProps prop in props)
            {
                Product p = new Product(prop);
                products.Add(p);
            }
            return products;
        }

        protected override void SetDefaultProperties() { }

        protected override void SetRequiredRules()
        {
            mRules.RuleBroken("ProductCode", true);
            mRules.RuleBroken("Description", true);
            mRules.RuleBroken("UnitPrice", true);
            mRules.RuleBroken("OnHandQuantity", true);
        }

        protected override void SetUp()
        {
            mProps = new ProductProps();
            mOldProps = new ProductProps();

            mdbReadable = new ProductDB();
            mdbWriteable = new ProductDB();
        }

        #region constructors
        public Product() : base() { }

        public Product(int key) : base(key) { }

        private Product(ProductProps props) : base(props) { }
        #endregion
    }
}
