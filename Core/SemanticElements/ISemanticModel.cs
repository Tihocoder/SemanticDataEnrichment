using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticDataEnrichment.Core.SemanticElements
{
	public interface ISemanticModel
	{
		IEnumerable<SemanticElement> SemanticElements { get; }
		string CreateSemanticHTML();
	}
}
