using System;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.Mcut
{
	public static unsafe class McutNative
	{
		/// <summary>
		/// This method creates a context object, which is a handle used by a client application to control the API state and access data.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL
		/// -# Failure to allocate resources
		/// -# 
		/// defines an invalid bitfield.
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcCreateContext(IntPtr* pContext, uint flags);

		/// <summary>
		/// ::mcDebugMessageCallback sets the current debug output callback function to the function whose address is
		/// given in callback.
		/// This function is defined to have the same calling convention as the MCUT API functions. In most cases
		/// this is defined as MCAPI_ATTR, although it will vary depending on platform, language and compiler.
		/// Each time a debug message is generated the debug callback function will be invoked with source, type,
		/// and severity associated with the message, and length set to the length of debug message whose
		/// character string is in the array pointed to by message userParam will be set to the value passed in
		/// the userParam parameter to the most recent call to mcDebugMessageCallback.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// is NULL.
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcDebugMessageCallback(IntPtr context, pfn_mcDebugOutput_CALLBACK cb, void* userParam);

		/// <summary>
		/// Control the reporting of debug messages in a debug context.
		/// ::mcDebugMessageControl controls the reporting of debug messages generated by a debug context. The parameters 
		/// source, type and severity form a filter to select messages from the pool of potential messages generated by 
		/// the MCUT library.
		/// may be #MC_DEBUG_SOURCE_API, #MC_DEBUG_SOURCE_KERNEL to select messages 
		/// generated by usage of the MCUT API, the MCUT kernel or by the input, respectively. It may also take the 
		/// value #MC_DEBUG_SOURCE_ALL. If source is not #MC_DEBUG_SOURCE_ALL then only messages whose source matches 
		/// source will be referenced.
		/// may be one of #MC_DEBUG_TYPE_ERROR, #MC_DEBUG_TYPE_DEPRECATED_BEHAVIOR, or #MC_DEBUG_TYPE_OTHER to indicate 
		/// the type of messages describing MCUT errors, attempted use of deprecated features, and other types of messages, 
		/// respectively. It may also take the value #MC_DONT_CARE. If type is not #MC_DEBUG_TYPE_ALL then only messages whose 
		/// type matches type will be referenced.
		/// may be one of #MC_DEBUG_SEVERITY_LOW, #MC_DEBUG_SEVERITY_MEDIUM, or #MC_DEBUG_SEVERITY_HIGH to 
		/// select messages of low, medium or high severity messages or to #MC_DEBUG_SEVERITY_NOTIFICATION for notifications. 
		/// It may also take the value #MC_DEBUG_SEVERITY_ALL. If severity is not #MC_DEBUG_SEVERITY_ALL then only 
		/// messages whose severity matches severity will be referenced.
		/// If 
		/// is true then messages that match the filter formed by source, type and severity are enabled. 
		/// Otherwise, those messages are disabled.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// is not a value define in ::McDebugSource.
		/// -# 
		/// is not a value define in ::McDebugType.
		/// -# 
		/// is not a value define in ::McDebugSeverity.
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcDebugMessageControl(IntPtr context, McDebugSource source, McDebugType type, McDebugSeverity severity, bool enabled);

		/// <summary>
		/// This function specifies the two mesh objects to operate on. The 'source mesh' is the mesh to be cut 
		/// (i.e. partitioned) along intersection paths prescribed by the 'cut mesh'. 
		/// An example of usage:
		/// Error codes
		/// - ::MC_NO_ERROR  
		/// -# proper exit 
		/// - ::MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// contains an invalid value.
		/// -# A vertex index in 
		/// or 
		/// is out of bounds.
		/// -# Invalid face/polygon definition (vertex list) implying non-manifold mesh 
		/// or 
		/// is out of bounds.
		/// -# The MC_DISPATCH_VERTEX_ARRAY_... value has not been specified in 
		/// -# An input mesh contains multiple connected components.
		/// -# 
		/// is NULL.
		/// -# 
		/// is NULL.
		/// -# 
		/// is NULL.
		/// -# 
		/// is less than three.
		/// -# 
		/// is less than one.
		/// -# 
		/// is NULL.
		/// -# 
		/// is NULL.
		/// -# 
		/// is NULL.
		/// -# 
		/// is less than three.
		/// -# 
		/// is less than one.
		/// -# ::MC_DISPATCH_ENFORCE_GENERAL_POSITION is not set and: 1) Found two intersecting edges between the source-mesh and the cut-mesh and/or 2) An intersection test between a face and an edge failed because an edge vertex only touches (but does not penetrate) the face, and/or 3) One or more source-mesh vertices are colocated with one or more cut-mesh vertices.
		/// - ::MC_OUT_OF_MEMORY
		/// -# Insufficient memory to perform operation.
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcDispatch(IntPtr context, uint flags, void* pSrcMeshVertices, uint* pSrcMeshFaceIndices, uint* pSrcMeshFaceSizes, uint numSrcMeshVertices, uint numSrcMeshFaces, void* pCutMeshVertices, uint* pCutMeshFaceIndices, uint* pCutMeshFaceSizes, uint numCutMeshVertices, uint numCutMeshFaces);

		/// <summary>
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// is greater than the returned size of data type queried
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcGetInfo(IntPtr context, uint info, ulong bytes, void* pMem, ulong* pNumBytes);

		/// <summary>
		/// This function will return an array of connected components matching the given description of flags.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// is not a value in ::McConnectedComponentType.
		/// -# 
		/// and 
		/// are both NULL.
		/// -# 
		/// is zero and 
		/// is not NULL.
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcGetConnectedComponents(IntPtr context, McConnectedComponentType connectedComponentType, uint numEntries, IntPtr* pConnComps, uint* numConnComps);

		/// <summary>
		/// The connected component queries described in the ::McConnectedComponentData should return the same information for a connected component returned by ::mcGetConnectedComponents.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// is not a value in ::McConnectedComponentType.
		/// -# 
		/// and 
		/// are both NULL (or not NULL).
		/// -# 
		/// is zero and 
		/// is not NULL.
		/// -# 
		/// is MC_CONNECTED_COMPONENT_DATA_VERTEX_MAP when 
		/// dispatch flags did not include flag MC_DISPATCH_INCLUDE_VERTEX_MAP
		/// -# 
		/// is MC_CONNECTED_COMPONENT_DATA_FACE_MAP when 
		/// dispatch flags did not include flag MC_DISPATCH_INCLUDE_FACE_MAP
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcGetConnectedComponentData(IntPtr context, IntPtr connCompId, uint flags, ulong bytes, void* pMem, ulong* pNumBytes);

		/// <summary>
		/// If 
		/// is zero and 
		/// is NULL, the memory of all connected components associated with the context is freed.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// -# 
		/// is zero and 
		/// is not NULL (and vice versa).
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcReleaseConnectedComponents(IntPtr context, uint numConnComps, IntPtr* pConnComps);

		/// <summary>
		/// This function ensures that all the state attached to context (such as unreleased connected components, and threads) are released, and the memory is deleted.
		/// An example of usage:
		/// Error codes
		/// - MC_NO_ERROR  
		/// -# proper exit 
		/// - MC_INVALID_VALUE 
		/// -# 
		/// is NULL or 
		/// is not an existing context.
		/// </summary>
		[DllImport("mcut", CallingConvention = CallingConvention.Cdecl)]
		public static extern McResult mcReleaseContext(IntPtr context);

	}
}
