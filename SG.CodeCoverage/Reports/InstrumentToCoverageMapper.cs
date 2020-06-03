﻿using SG.CodeCoverage.Metadata;
using SG.CodeCoverage.Metadata.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.CodeCoverage.Reports
{
    public static class InstrumentToCoverageMapper
    {
        public static CoverageMethodResult ToMethodCoverage(this InstrumentedMethodMap method, int visitCount)
        {
            return new CoverageMethodResult(
                method.FullName,
                method.Index,
                method.Source,
                method.StartLine,
                method.EndLine,
                visitCount
            );
        }

        public static CoverageTypeResult ToTypeCoverage(this InstrumentedTypeMap type, int[] typeHits)
        {
            return new CoverageTypeResult(
                type.FullName,
                type.Index,
                type.Methods.Select(x => x.ToMethodCoverage(typeHits[x.Index])).ToList().AsReadOnly()
            );
        }

        public static CoverageAssemblyResult ToAssemblyCoverage(this InstrumentedAssemblyMap assembly, int[][] hits)
        {
            return new CoverageAssemblyResult(
                assembly.Name,
                assembly.Types.Select(x => x.ToTypeCoverage(hits[x.Index])).ToList().AsReadOnly()
            );
        }

        public static CoverageResult ToCoverageResult(this InstrumentationMap map, int[][] hits)
        {
            return new CoverageResult(
                map.Version,
                map.UniqueId,
                map.Assemblies.Select(x => x.ToAssemblyCoverage(hits)).ToList().AsReadOnly()
            );
        }

    }
}
