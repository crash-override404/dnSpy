﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel.Composition;
using dnSpy.Contracts.Settings;

namespace dnSpy.Contracts.Debugger.Breakpoints.Code {
	/// <summary>
	/// <see cref="DbgBreakpointLocation"/> serializer. Use <see cref="ExportDbgBreakpointLocationSerializerAttribute"/>
	/// to export an instance.
	/// </summary>
	public abstract class DbgBreakpointLocationSerializer {
		/// <summary>
		/// Serializes <paramref name="location"/>
		/// </summary>
		/// <param name="section">Destination section</param>
		/// <param name="location">Breakpoint location</param>
		public abstract void Serialize(ISettingsSection section, DbgBreakpointLocation location);

		/// <summary>
		/// Deserializes a breakpoint or returns null if it failed
		/// </summary>
		/// <param name="section">Serialized section</param>
		/// <returns></returns>
		public abstract DbgBreakpointLocation Deserialize(ISettingsSection section);
	}

	/// <summary>Metadata</summary>
	public interface IDbgBreakpointLocationSerializerMetadata {
		/// <summary>See <see cref="ExportDbgBreakpointLocationSerializerAttribute.Types"/></summary>
		string[] Types { get; }
	}

	/// <summary>
	/// Exports a <see cref="DbgBreakpointLocationSerializer"/> instance
	/// </summary>
	[MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExportDbgBreakpointLocationSerializerAttribute : ExportAttribute, IDbgBreakpointLocationSerializerMetadata {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="type">Type (compared against <see cref="DbgBreakpointLocation.Type"/>), see <see cref="PredefinedDbgBreakpointLocationTypes"/></param>
		public ExportDbgBreakpointLocationSerializerAttribute(string type)
			: base(typeof(DbgBreakpointLocationSerializer)) => Types = new[] { type ?? throw new ArgumentNullException(nameof(type)) };

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="types">Types (compared against <see cref="DbgBreakpointLocation.Type"/>), see <see cref="PredefinedDbgBreakpointLocationTypes"/></param>
		public ExportDbgBreakpointLocationSerializerAttribute(string[] types)
			: base(typeof(DbgBreakpointLocationSerializer)) => Types = types ?? throw new ArgumentNullException(nameof(types));

		/// <summary>
		/// Types (compared against <see cref="DbgBreakpointLocation.Type"/>), see <see cref="PredefinedDbgBreakpointLocationTypes"/>
		/// </summary>
		public string[] Types { get; }
	}
}
