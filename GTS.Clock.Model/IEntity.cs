using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;

namespace GTS.Clock.Model
{
	public interface IEntity
	{
		decimal ID { get; set; }
	}
}
