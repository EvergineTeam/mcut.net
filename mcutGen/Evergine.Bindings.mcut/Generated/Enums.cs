using System;

namespace Evergine.Bindings.Mcut
{
	/// <summary>
	/// This enum structure defines the possible return values of API functions (integer). The values identify whether a function executed successfully or returned with an error.
	/// </summary>
	public enum McResult
	{

		/// <summary>
		/// The function was successfully executed. 
		/// </summary>
		MC_NO_ERROR = 0,

		/// <summary>
		/// An internal operation could not be executed successively. 
		/// </summary>
		MC_INVALID_OPERATION = -2,

		/// <summary>
		/// An invalid value has been passed to the API. 
		/// </summary>
		MC_INVALID_VALUE = -4,

		/// <summary>
		/// Memory allocation operation cannot allocate memory. 
		/// </summary>
		MC_OUT_OF_MEMORY = -8,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_RESULT_MAX_ENUM = -1,
	}

	/// <summary>
	/// This enum structure defines the possible types of connected components which can be queried from the API after a dispatch call. 
	/// </summary>
	public enum McConnectedComponentType
	{

		/// <summary>
		/// A connected component that originates from the source-mesh. 
		/// </summary>
		MC_CONNECTED_COMPONENT_TYPE_FRAGMENT = 1,

		/// <summary>
		/// A connected component that is originates from the cut-mesh. 
		/// </summary>
		MC_CONNECTED_COMPONENT_TYPE_PATCH = 4,

		/// <summary>
		/// A connected component representing an input mesh (source-mesh or cut-mesh), but with additional vertices and edges that are introduced as as a result of the cut (i.e. the intersection contour/curve). 
		/// </summary>
		MC_CONNECTED_COMPONENT_TYPE_SEAM = 8,

		/// <summary>
		/// A connected component that is copy of an input mesh (source-mesh or cut-mesh). Such a connected component may contain new faces and vertices, which will happen if MCUT internally performs polygon partitioning. Polygon partitioning occurs when an input mesh intersects the other without severing at least one edge. An example is splitting a tetrahedron (source-mesh) in two parts using one large triangle (cut-mesh): in this case, the large triangle would be partitioned into two faces to ensure that at least one of this cut-mesh are severed by the tetrahedron. This is what allows MCUT to reconnect topology after the cut. 
		/// </summary>
		MC_CONNECTED_COMPONENT_TYPE_INPUT = 16,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_CONNECTED_COMPONENT_TYPE_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the possible locations where a fragment can be relative to the cut-mesh. Note that the labels of 'above' or 'below' here are defined with-respect-to the winding-order (and hence, normal orientation) of the cut-mesh. 
	/// </summary>
	public enum McFragmentLocation
	{

		/// <summary>
		/// Fragment is located above the cut-mesh. 
		/// </summary>
		MC_FRAGMENT_LOCATION_ABOVE = 1,

		/// <summary>
		/// Fragment is located below the cut-mesh. 
		/// </summary>
		MC_FRAGMENT_LOCATION_BELOW = 2,

		/// <summary>
		/// Fragment is located neither above nor below the cut-mesh. That is, it was produced due to a partial cut intersection. 
		/// </summary>
		MC_FRAGMENT_LOCATION_UNDEFINED = 4,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_FRAGMENT_LOCATION_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the possible configurations that a fragment will be in regarding the hole-filling process. Here, hole-filling refers to the stage/phase when holes produced by a cut are filled with a subset of polygons of the cut-mesh.
	/// </summary>
	public enum McFragmentSealType
	{

		/// <summary>
		/// Holes are completely sealed (watertight). 
		/// </summary>
		MC_FRAGMENT_SEAL_TYPE_COMPLETE = 1,

		/// <summary>
		/// Holes are not sealed (gaping hole). 
		/// </summary>
		MC_FRAGMENT_SEAL_TYPE_NONE = 4,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_FRAGMENT_SEAL_TYPE_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the possible locations where a patch can be relative to the source-mesh. Note that the labels of 'inside' or 'outside' here are defined with-respect-to the winding-order (and hence, normal orientation) of the source-mesh.
	/// </summary>
	public enum McPatchLocation
	{

		/// <summary>
		/// Patch is located on the interior of the source-mesh (used to seal holes). 
		/// </summary>
		MC_PATCH_LOCATION_INSIDE = 1,

		/// <summary>
		/// Patch is located on the exterior of the source-mesh. Rather than hole-filling these patches seal from the outside so-as to extrude the cut.
		/// </summary>
		MC_PATCH_LOCATION_OUTSIDE = 2,

		/// <summary>
		/// Patch is located neither on the interior nor exterior of the source-mesh. 
		/// </summary>
		MC_PATCH_LOCATION_UNDEFINED = 4,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_PATCH_LOCATION_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the possible origins of a seam connected component, which can be either the source-mesh or the cut-mesh. 
	/// </summary>
	public enum McSeamOrigin
	{

		/// <summary>
		/// Seam connected component is from the input source-mesh. 
		/// </summary>
		MC_SEAM_ORIGIN_SRCMESH = 1,

		/// <summary>
		/// Seam connected component is from the input cut-mesh. 
		/// </summary>
		MC_SEAM_ORIGIN_CUTMESH = 2,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_SEAM_ORIGIN_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the possible origins of an input connected component, which can be either the source-mesh or the cut-mesh.
	/// Note: the number of elements (faces and vertices) in an input connected component will be the same [or greater] than the corresponding user-provided input mesh from which the respective connected component came from. The input connect component will contain more elements if MCUT detected an intersection configuration where one input mesh will create a hole in a face of the other input mesh but without severing it edges (and vice versa). 
	/// </summary>
	public enum McInputOrigin
	{

		/// <summary>
		/// Input connected component from the input source mesh.
		/// </summary>
		MC_INPUT_ORIGIN_SRCMESH = 1,

		/// <summary>
		/// Input connected component from the input cut mesh. 
		/// </summary>
		MC_INPUT_ORIGIN_CUTMESH = 2,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_INPUT_ORIGIN_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the different types of data that are associated with a connected component and can be queried from the API after a dispatch call.
	/// </summary>
	public enum McConnectedComponentData
	{

		/// <summary>
		/// List of vertex coordinates as an array of 32 bit floating-point numbers. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_VERTEX_FLOAT = 2,

		/// <summary>
		/// List of vertex coordinates as an array of 64 bit floating-point numbers. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_VERTEX_DOUBLE = 4,

		/// <summary>
		/// List of faces as an array of indices. Each face can also be understood as a "planar straight line graph" (PSLG), which is a collection of vertices and segments that lie on the same plane. Segments are edges whose endpoints are vertices in the PSLG.
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FACE = 32,

		/// <summary>
		/// List of face sizes (vertices per face) as an array. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FACE_SIZE = 64,

		/// <summary>
		/// List of edges as an array of indices. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_EDGE = 256,

		/// <summary>
		/// The type of a connected component (See also: ::McConnectedComponentType.). 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_TYPE = 512,

		/// <summary>
		/// The location of a fragment connected component with respect to the cut mesh (See also: ::McFragmentLocation). 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FRAGMENT_LOCATION = 1024,

		/// <summary>
		/// The location of a patch with respect to the source mesh (See also: ::McPatchLocation).
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_PATCH_LOCATION = 2048,

		/// <summary>
		/// The Hole-filling configuration of a fragment connected component (See also: ::McFragmentSealType). 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FRAGMENT_SEAL_TYPE = 4096,

		/// <summary>
		/// List of seam-vertices as an array of indices.
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_SEAM_VERTEX = 8192,

		/// <summary>
		/// The input mesh (source- or cut-mesh) from which a "seam" is derived (See also: ::McSeamOrigin). 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_ORIGIN = 16384,

		/// <summary>
		/// List of a subset of vertex indices from one of the input meshes (source-mesh or the cut-mesh). Each value will be the index of an input mesh vertex or MC_UNDEFINED_VALUE. This index-value corresponds to the connected component vertex at the accessed index. The value at index 0 of the queried array is the index of the vertex in the original input mesh. In order to clearly distinguish indices of the cut mesh from those of the source mesh, this index value corresponds to a cut mesh vertex index if it is great-than-or-equal-to the number of source-mesh vertices. Intersection points are mapped to MC_UNDEFINED_VALUE. The input mesh (i.e. source- or cut-mesh) will be deduced by the user from the type of connected component with which the information is queried. The input connected component (source-mesh or cut-mesh) that is referred to must be one stored internally by MCUT (i.e. a connected component queried from the API via ::McInputOrigin), to ensure consistency with any modification done internally by MCUT. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_VERTEX_MAP = 32768,

		/// <summary>
		/// List a subset of face indices from one of the input meshes (source-mesh or the cut-mesh). Each value will be the index of an input mesh face. This index-value corresponds to the connected component face at the accessed index. Example: the value at index 0 of the queried array is the index of the face in the original input mesh. Note that all faces are mapped to a defined value. In order to clearly distinguish indices of the cut mesh from those of the source mesh, an input-mesh face index value corresponds to a cut-mesh vertex-index if it is great-than-or-equal-to the number of source-mesh faces. The input connected component (source-mesh or cut-mesh) that is referred to must be one stored internally by MCUT (i.e. a connected component queried from the API via ::McInputOrigin), to ensure consistency with any modification done internally by MCUT. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FACE_MAP = 65536,

		/// <summary>
		/// List of adjacent faces (their indices) per face.
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FACE_ADJACENT_FACE = 131072,

		/// <summary>
		/// List of adjacent-face-list sizes (number of adjacent faces per face).
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FACE_ADJACENT_FACE_SIZE = 262144,

		/// <summary>
		/// List of 3*N triangulated face indices, where N is the number of triangles that are produced using a [Constrained] Delaunay triangulation. Such a triangulation is similar to a Delaunay triangulation, but each (non-triangulated) face segment is present as a single edge in the triangulation. A constrained Delaunay triangulation is not truly a Delaunay triangulation. Some of its triangles might not be Delaunay, but they are all constrained Delaunay. 
		/// </summary>
		MC_CONNECTED_COMPONENT_DATA_FACE_TRIANGULATION = 524288,
	}

	/// <summary>
	/// This enum structure defines the sources from which a message in a debug log may originate.
	/// </summary>
	public enum McDebugSource
	{

		/// <summary>
		/// messages generated by usage of the MCUT API. 
		/// </summary>
		MC_DEBUG_SOURCE_API = 1,

		/// <summary>
		/// messages generated by internal logic implementing the kernel. 
		/// </summary>
		MC_DEBUG_SOURCE_KERNEL = 2,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_DEBUG_SOURCE_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the types of debug a message relating to an error. 
	/// </summary>
	public enum McDebugType
	{

		/// <summary>
		/// Explicit error message.
		/// </summary>
		MC_DEBUG_TYPE_ERROR = 1,

		/// <summary>
		/// Attempted use of deprecated features.
		/// </summary>
		MC_DEBUG_TYPE_DEPRECATED_BEHAVIOR = 2,

		/// <summary>
		/// Other types of messages,.
		/// </summary>
		MC_DEBUG_TYPE_OTHER = 4,

		/// <summary>
		/// Wildcard (match all) . 
		/// </summary>
		MC_DEBUG_TYPE_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the different severities of error: low, medium or high severity messages.
	/// </summary>
	public enum McDebugSeverity
	{

		/// <summary>
		/// All MCUT Errors, mesh conversion/parsing errors, or undefined behavior.
		/// </summary>
		MC_DEBUG_SEVERITY_HIGH = 1,

		/// <summary>
		/// Major performance warnings, debugging warnings, or the use of deprecated functionality.
		/// </summary>
		MC_DEBUG_SEVERITY_MEDIUM = 2,

		/// <summary>
		/// Redundant state change, or unimportant undefined behavior.
		/// </summary>
		MC_DEBUG_SEVERITY_LOW = 4,

		/// <summary>
		/// Anything that isn't an error or performance issue.
		/// </summary>
		MC_DEBUG_SEVERITY_NOTIFICATION = 8,

		/// <summary>
		/// Match all (wildcard).
		/// </summary>
		MC_DEBUG_SEVERITY_ALL = -1,
	}

	/// <summary>
	/// This enum structure defines the flags with which a context can be created.
	/// </summary>
	public enum McContextCreationFlags
	{

		/// <summary>
		/// Enable debug mode (message logging etc.).
		/// </summary>
		MC_DEBUG = 1,
	}

	/// <summary>
	/// This enum structure defines the flags indicating MCUT is to interprete input data, and execute the cutting pipeline.
	/// </summary>
	public enum McDispatchFlags
	{

		/// <summary>
		/// Interpret the input mesh vertices as arrays of 32-bit floating-point numbers.
		/// </summary>
		MC_DISPATCH_VERTEX_ARRAY_FLOAT = 1,

		/// <summary>
		/// Interpret the input mesh vertices as arrays of 64-bit floating-point numbers.
		/// </summary>
		MC_DISPATCH_VERTEX_ARRAY_DOUBLE = 2,

		/// <summary>
		/// Require that all intersection paths/curves/contours partition the source-mesh into two (or more) fully disjoint parts. Otherwise, ::mcDispatch is a no-op. This flag enforces the requirement that only through-cuts are valid cuts i.e it disallows partial cuts. NOTE: This flag may not be used with ::MC_DISPATCH_FILTER_FRAGMENT_LOCATION_UNDEFINED.
		/// </summary>
		MC_DISPATCH_REQUIRE_THROUGH_CUTS = 4,

		/// <summary>
		/// Compute connected-component-to-input mesh vertex-id maps. See also: ::MC_CONNECTED_COMPONENT_DATA_VERTEX_MAP 
		/// </summary>
		MC_DISPATCH_INCLUDE_VERTEX_MAP = 8,

		/// <summary>
		/// Compute connected-component-to-input mesh face-id maps. . See also: ::MC_CONNECTED_COMPONENT_DATA_FACE_MAP
		/// </summary>
		MC_DISPATCH_INCLUDE_FACE_MAP = 16,

		/// <summary>
		/// Compute fragments that are above the cut-mesh.
		/// </summary>
		MC_DISPATCH_FILTER_FRAGMENT_LOCATION_ABOVE = 32,

		/// <summary>
		/// Compute fragments that are below the cut-mesh.
		/// </summary>
		MC_DISPATCH_FILTER_FRAGMENT_LOCATION_BELOW = 64,

		/// <summary>
		/// Compute fragments that are partially cut i.e. neither above nor below the cut-mesh. NOTE: This flag may not be used with ::MC_DISPATCH_REQUIRE_THROUGH_CUTS. 
		/// </summary>
		MC_DISPATCH_FILTER_FRAGMENT_LOCATION_UNDEFINED = 128,

		/// <summary>
		/// Compute fragments that are fully sealed (hole-filled) on the interior.   
		/// </summary>
		MC_DISPATCH_FILTER_FRAGMENT_SEALING_INSIDE = 256,

		/// <summary>
		/// Compute fragments that are fully sealed (hole-filled) on the exterior.  
		/// </summary>
		MC_DISPATCH_FILTER_FRAGMENT_SEALING_OUTSIDE = 512,

		/// <summary>
		/// Compute fragments that are not sealed (holes not filled).
		/// </summary>
		MC_DISPATCH_FILTER_FRAGMENT_SEALING_NONE = 1024,

		/// <summary>
		/// Compute patches on the inside of the source mesh (those used to fill holes).
		/// </summary>
		MC_DISPATCH_FILTER_PATCH_INSIDE = 2048,

		/// <summary>
		/// Compute patches on the outside of the source mesh.
		/// </summary>
		MC_DISPATCH_FILTER_PATCH_OUTSIDE = 4096,

		/// <summary>
		/// Compute the seam which is the same as the source-mesh but with new edges placed along the cut path. Note: a seam from the source-mesh will only be computed if the dispatch operation computes a complete (through) cut.
		/// </summary>
		MC_DISPATCH_FILTER_SEAM_SRCMESH = 8192,

		/// <summary>
		/// Compute the seam which is the same as the cut-mesh but with new edges placed along the cut path. Note: a seam from the cut-mesh will only be computed if the dispatch operation computes a complete (through) cut.
		/// </summary>
		MC_DISPATCH_FILTER_SEAM_CUTMESH = 16384,

		/// <summary>
		/// </summary>
		MC_DISPATCH_FILTER_ALL = 32736,

		/// <summary>
		/// Allow MCUT to perturb the cut-mesh if the inputs are not in general position. 
		/// MCUT is formulated for inputs in general position. Here the notion of general position is defined with
		/// respect to the orientation predicate (as evaluated on the intersecting polygons). Thus, a set of points 
		/// is in general position if no three points are collinear and also no four points are coplanar.
		/// MCUT uses the "GENERAL_POSITION_VIOLATION" flag to inform of when to use perturbation (of the
		/// cut-mesh) so as to bring the input into general position. In such cases, the idea is to solve the cutting
		/// problem not on the given input, but on a nearby input. The nearby input is obtained by perturbing the given
		/// input. The perturbed input will then be in general position and, since it is near the original input,
		/// the result for the perturbed input will hopefully still be useful.  This is justified by the fact that
		/// the task of MCUT is not to decide whether the input is in general position but rather to make perturbation
		/// on the input (if) necessary within the available precision of the computing device. 
		/// </summary>
		MC_DISPATCH_ENFORCE_GENERAL_POSITION = 32768,
	}

	/// <summary>
	/// This enum structure defines the flags which are used to query for specific information about the state of the API and/or a given context. 
	/// </summary>
	public enum McQueryFlags
	{

		/// <summary>
		/// Flags used to create a context.
		/// </summary>
		MC_CONTEXT_FLAGS = 1,

		/// <summary>
		/// wildcard.
		/// </summary>
		MC_DONT_CARE = 2,
	}

}
