using System;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.Mcut
{
	/// <summary>
	/// The callback function should have this prototype (in C), or be otherwise compatible with such a prototype.
	/// </summary>
	public unsafe delegate void pfn_mcDebugOutput_CALLBACK(
		 McDebugSource source,
		 McDebugType type,
		 uint id,
		 McDebugSeverity severity,
		 UIntPtr length,
		 char* message,
		 void* userParam);

}
