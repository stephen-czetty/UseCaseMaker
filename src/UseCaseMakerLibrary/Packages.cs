using System;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
	public class Packages : IdentificableObjectCollection<Package>
	{
		internal Packages(Package owner)
		{
			base.Owner = owner;
		}

		public IIdentificableObject FindElementByUniqueID(String uniqueID)
		{
			IIdentificableObject element = null;

			if(this.UniqueId == uniqueID)
			{
				return this;
			}

			foreach(Package child in this)
			{
				if(child.UniqueId == uniqueID)
				{
					element = child;
					break;
				}
				if(child.Actors.UniqueId == uniqueID)
				{
					element = child.Actors;
					break;
				}
				element = child.Actors.FindByUniqueId(uniqueID);
				if(element != null)
				{
					break;
				}
				if(child.UseCases.UniqueId == uniqueID)
				{
					element = child.UseCases;
					break;
				}
				element = child.UseCases.FindByUniqueId(uniqueID);
				if(element != null)
				{
					break;
				}
				if(child.Requirements.UniqueId == uniqueID)
				{
					element = child.Requirements;
					break;
				}
				element = child.Requirements.FindByUniqueId(uniqueID);
				if(element != null)
				{
					break;
				}
				if(child.Packages.Count > 0)
				{
					element = child.Packages.FindElementByUniqueID(uniqueID);
					if(element != null)
					{
						break;
					}
				}
			}

			return element;
		}

		public IIdentificableObject FindElementByName(String name)
		{
			IIdentificableObject element = null;

			if(this.Name == name)
			{
				return this;
			}

			foreach(Package child in this)
			{
				if(child.Name == name)
				{
					element = child;
					break;
				}
				if(child.Actors.Name == name)
				{
					element = child.Actors;
					break;
				}
				element = child.Actors.FindByName(name);
				if(element != null)
				{
					break;
				}
				if(child.UseCases.Name == name)
				{
					element = child.UseCases;
					break;
				}
				element = child.UseCases.FindByName(name);
				if(element != null)
				{
					break;
				}
				if(child.Requirements.Name == name)
				{
					element = child.Requirements;
					break;
				}
				element = child.Requirements.FindByName(name);
				if(element != null)
				{
					break;
				}
				if(child.Packages.Count > 0)
				{
					element = child.Packages.FindElementByName(name);
					if(element != null)
					{
						break;
					}
				}
			}

			return element;
		}

		public IIdentificableObject FindElementByPath(String path)
		{
			IIdentificableObject element = null;

			if(this.Path == path)
			{
				return this;
			}

			foreach(Package child in this)
			{
				if(child.Path == path)
				{
					element = child;
					break;
				}
				if(child.Actors.Path == path)
				{
					element = child.Actors;
					break;
				}
				element = child.Actors.FindByPath(path);
				if(element != null)
				{
					break;
				}
				if(child.UseCases.Path == path)
				{
					element = child.UseCases;
					break;
				}
				element = child.UseCases.FindByPath(path);
				if(element != null)
				{
					break;
				}
				if(child.Requirements.Path == path)
				{
					element = child.Requirements;
					break;
				}
				element = child.Requirements.FindByPath(path);
				if(element != null)
				{
					return element;
				}
				if(child.Packages.Count > 0)
				{
					element = child.Packages.FindElementByPath(path);
					if(element != null)
					{
						break;
					}
				}
			}

			return element;
		}
	}
}
