using System;

using MMABooksTools;
using MMABooksProps;
using MMABooksDB;

using System.Collections.Generic;

namespace MMABooksBusiness
{
    public class Customer : BaseBusiness
    {
        public int CustomerID
        {
            get
            {
                return ((CustomerProps)mProps).CustomerID;
            }
        }

        public String Name
        {
            get
            {
                return((CustomerProps)mProps).Name;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).Name))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("Name", false);
                        ((CustomerProps)mProps).Name = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 20 characters long.");
                    }
                }
            }
        }

        public String Address
        {
            get
            {
                return ((CustomerProps)mProps).Address;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).Address))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("Address", false);
                        ((CustomerProps)mProps).Address = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 20 characters long.");
                    }
                }
            }
        }

        public String City
        {
            get
            {
                return ((CustomerProps)mProps).City;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).City))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("City", false);
                        ((CustomerProps)mProps).City = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 20 characters long.");
                    }
                }
            }
        }

        public String State
        {
            get
            {
                return ((CustomerProps)mProps).State;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).State))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 2)
                    {
                        mRules.RuleBroken("State", false);
                        ((CustomerProps)mProps).State = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 20 characters long.");
                    }
                }
            }
        }
        public String ZipCode
        {
            get
            {
                return ((CustomerProps)mProps).ZipCode;
            }

            set
            {
                if (!(value == ((CustomerProps)mProps).ZipCode))
                {
                    if (value.Trim().Length >= 1 && value.Trim().Length <= 20)
                    {
                        mRules.RuleBroken("ZipCode", false);
                        ((CustomerProps)mProps).ZipCode = value;
                        mIsDirty = true;
                    }

                    else
                    {
                        throw new ArgumentOutOfRangeException("Name must be no more than 20 characters long.");
                    }
                }
            }
        }

        public override object GetList()
        {
            List<Customer> customers = new List<Customer>();
            List<CustomerProps> props = new List<CustomerProps>();


            props = (List<CustomerProps>)mdbReadable.RetrieveAll();
            foreach (CustomerProps prop in props)
            {
                Customer c = new Customer(prop);
                customers.Add(c);
            }

            return customers;
        }

        protected override void SetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        protected override void SetRequiredRules()
        {
            mRules.RuleBroken("CustomerID", true);
            mRules.RuleBroken("Name", true);
            mRules.RuleBroken("Address", true);
            mRules.RuleBroken("City", true);
            mRules.RuleBroken("State", true);
            mRules.RuleBroken("ZipCode", true);
        }

        protected override void SetUp()
        {
            mProps = new CustomerProps();
            mOldProps = new CustomerProps();

            mdbReadable = new CustomerDB();
            mdbWriteable = new CustomerDB();
        }

        #region constructors
        public Customer() : base()
        {
        }

        public Customer(string key)
            : base(key)
        {
        }

        private Customer(CustomerProps props)
            : base(props)
        {
        }

        #endregion
    }
}
