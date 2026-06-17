using AutoMapper;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Forms.Parameters.Expressions;

namespace LogicBuilder.EntityFrameworkCore.Mapping
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ExpressionParameterToDescriptorMappingProfile : Profile
    {
        public ExpressionParameterToDescriptorMappingProfile()
        {
            CreateMap<AddBinaryOperatorParameters, AddBinaryDescriptor>();
            CreateMap<AllOperatorParameters, AllDescriptor>();
            CreateMap<AndBinaryOperatorParameters, AndBinaryDescriptor>();
            CreateMap<AnyOperatorParameters, AnyDescriptor>();
            CreateMap<AsEnumerableOperatorParameters, AsEnumerableDescriptor>();
            CreateMap<AsQueryableOperatorParameters, AsQueryableDescriptor>();
            CreateMap<AverageOperatorParameters, AverageDescriptor>();
            CreateMap<BinaryOperatorParameters, BinaryDescriptor>();
            CreateMap<CastOperatorParameters, CastDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<CeilingOperatorParameters, CeilingDescriptor>();
            CreateMap<CollectionCastOperatorParameters, CollectionCastDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<CollectionConstantOperatorParameters, CollectionConstantDescriptor>()
                .ForMember(dest => dest.ElementType, opts => opts.Ignore())
                .ForCtorParam("elementType", opts => opts.MapFrom(x => x.ElementType.AssemblyQualifiedName));
            CreateMap<CollectionOfTypeOperatorParameters, CollectionOfTypeDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<ConcatOperatorParameters, ConcatDescriptor>();
            CreateMap<ConstantOperatorParameters, ConstantDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type == null ? null : x.Type.AssemblyQualifiedName));
            CreateMap<ContainsOperatorParameters, ContainsDescriptor>();
            CreateMap<ConvertCharArrayToStringOperatorParameters, ConvertCharArrayToStringDescriptor>();
            CreateMap<ConvertOperatorParameters, ConvertDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<ConvertToEnumOperatorParameters, ConvertToEnumDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<ConvertToNullableUnderlyingValueOperatorParameters, ConvertToNullableUnderlyingValueDescriptor>();
            CreateMap<ConvertToNumericDateOperatorParameters, ConvertToNumericDateDescriptor>();
            CreateMap<ConvertToNumericTimeOperatorParameters, ConvertToNumericTimeDescriptor>();
            CreateMap<ConvertToStringOperatorParameters, ConvertToStringDescriptor>();
            CreateMap<CountOperatorParameters, CountDescriptor>();
            CreateMap<CustomMethodOperatorParameters, CustomMethodDescriptor>()
                .ForMember(dest => dest.DeclaringType, opts => opts.Ignore())
                .ForCtorParam("declaringType", opts => opts.MapFrom(x => x.DeclaringType.AssemblyQualifiedName));
            CreateMap<DateOperatorParameters, DateDescriptor>();
            CreateMap<DayOperatorParameters, DayDescriptor>();
            CreateMap<DistinctOperatorParameters, DistinctDescriptor>();
            CreateMap<DivideBinaryOperatorParameters, DivideBinaryDescriptor>();
            CreateMap<EndsWithOperatorParameters, EndsWithDescriptor>();
            CreateMap<EnumerableSelectorLambdaOperatorParameters, EnumerableSelectorLambdaDescriptor>()
                .ForMember(dest => dest.SourceElementType, opts => opts.Ignore())
                .ForCtorParam("sourceElementType", opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName));
            CreateMap<EqualsBinaryOperatorParameters, EqualsBinaryDescriptor>();
            CreateMap<ExceptOperatorParameters, ExceptDescriptor>();
            CreateMap<FilterLambdaOperatorParameters, FilterLambdaDescriptor>()
                .ForMember(dest => dest.SourceElementType, opts => opts.Ignore())
                .ForCtorParam("sourceElementType", opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName));
            CreateMap<FirstOperatorParameters, FirstDescriptor>();
            CreateMap<FirstOrDefaultOperatorParameters, FirstOrDefaultDescriptor>();
            CreateMap<FloorOperatorParameters, FloorDescriptor>();
            CreateMap<FractionalSecondsOperatorParameters, FractionalSecondsDescriptor>();
            CreateMap<GreaterThanBinaryOperatorParameters, GreaterThanBinaryDescriptor>();
            CreateMap<GreaterThanOrEqualsBinaryOperatorParameters, GreaterThanOrEqualsBinaryDescriptor>();
            CreateMap<GroupByOperatorParameters, GroupByDescriptor>();
            CreateMap<HasOperatorParameters, HasDescriptor>();
            CreateMap<HourOperatorParameters, HourDescriptor>();
            CreateMap<IndexOfOperatorParameters, IndexOfDescriptor>();
            CreateMap<InOperatorParameters, InDescriptor>();
            CreateMap<IsOfOperatorParameters, IsOfDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.Ignore())
                .ForCtorParam("type", opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
            CreateMap<LastOperatorParameters, LastDescriptor>();
            CreateMap<LastOrDefaultOperatorParameters, LastOrDefaultDescriptor>();
            CreateMap<LengthOperatorParameters, LengthDescriptor>();
            CreateMap<LessThanBinaryOperatorParameters, LessThanBinaryDescriptor>();
            CreateMap<LessThanOrEqualsBinaryOperatorParameters, LessThanOrEqualsBinaryDescriptor>();
            CreateMap<MaxDateTimeOperatorParameters, MaxDateTimeDescriptor>();
            CreateMap<MaxOperatorParameters, MaxDescriptor>();
            CreateMap<MemberInitOperatorParameters, MemberInitDescriptor>()
                .ForMember(dest => dest.NewType, opts => opts.Ignore())
                .ForCtorParam("newType", opts => opts.MapFrom(x => x.NewType == null ? null : x.NewType.AssemblyQualifiedName));
            CreateMap<MemberSelectorOperatorParameters, MemberSelectorDescriptor>();
            CreateMap<MinDateTimeOperatorParameters, MinDateTimeDescriptor>();
            CreateMap<MinOperatorParameters, MinDescriptor>();
            CreateMap<MinuteOperatorParameters, MinuteDescriptor>();
            CreateMap<ModuloBinaryOperatorParameters, ModuloBinaryDescriptor>();
            CreateMap<MonthOperatorParameters, MonthDescriptor>();
            CreateMap<MultiplyBinaryOperatorParameters, MultiplyBinaryDescriptor>();
            CreateMap<NegateOperatorParameters, NegateDescriptor>();
            CreateMap<NotEqualsBinaryOperatorParameters, NotEqualsBinaryDescriptor>();
            CreateMap<NotOperatorParameters, NotDescriptor>();
            CreateMap<NowDateTimeOperatorParameters, NowDateTimeDescriptor>();
            CreateMap<OrBinaryOperatorParameters, OrBinaryDescriptor>();
            CreateMap<OrderByOperatorParameters, OrderByDescriptor>();
            CreateMap<ParameterOperatorParameters, ParameterDescriptor>();
            CreateMap<RoundOperatorParameters, RoundDescriptor>();
            CreateMap<SecondOperatorParameters, SecondDescriptor>();
            CreateMap<SelectManyOperatorParameters, SelectManyDescriptor>();
            CreateMap<SelectOperatorParameters, SelectDescriptor>();
            CreateMap<SelectorLambdaOperatorParameters, SelectorLambdaDescriptor>()
                .ForMember(dest => dest.SourceElementType, opts => opts.Ignore())
                .ForCtorParam("sourceElementType", opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName))
                .ForMember(dest => dest.BodyType, opts => opts.Ignore())
                .ForCtorParam("bodyType", opts => opts.MapFrom(x => x.BodyType == null ? null : x.BodyType.AssemblyQualifiedName));
            CreateMap<SingleOperatorParameters, SingleDescriptor>();
            CreateMap<SingleOrDefaultOperatorParameters, SingleOrDefaultDescriptor>();
            CreateMap<SkipOperatorParameters, SkipDescriptor>();
            CreateMap<StartsWithOperatorParameters, StartsWithDescriptor>();
            CreateMap<SubstringOperatorParameters, SubstringDescriptor>();
            CreateMap<SubtractBinaryOperatorParameters, SubtractBinaryDescriptor>();
            CreateMap<SumOperatorParameters, SumDescriptor>();
            CreateMap<TakeOperatorParameters, TakeDescriptor>();
            CreateMap<ThenByOperatorParameters, ThenByDescriptor>();
            CreateMap<TimeOperatorParameters, TimeDescriptor>();
            CreateMap<ToListOperatorParameters, ToListDescriptor>();
            CreateMap<ToLowerOperatorParameters, ToLowerDescriptor>();
            CreateMap<TotalOffsetMinutesOperatorParameters, TotalOffsetMinutesDescriptor>();
            CreateMap<TotalSecondsOperatorParameters, TotalSecondsDescriptor>();
            CreateMap<ToUpperOperatorParameters, ToUpperDescriptor>();
            CreateMap<TrimOperatorParameters, TrimDescriptor>();
            CreateMap<UnionOperatorParameters, UnionDescriptor>();
            CreateMap<WhereOperatorParameters, WhereDescriptor>();
            CreateMap<YearOperatorParameters, YearDescriptor>();

            CreateMap<IExpressionParameter, DescriptorBase>()
                .Include<AddBinaryOperatorParameters, AddBinaryDescriptor>()
                .Include<AllOperatorParameters, AllDescriptor>()
                .Include<AndBinaryOperatorParameters, AndBinaryDescriptor>()
                .Include<AnyOperatorParameters, AnyDescriptor>()
                .Include<AsEnumerableOperatorParameters, AsEnumerableDescriptor>()
                .Include<AsQueryableOperatorParameters, AsQueryableDescriptor>()
                .Include<AverageOperatorParameters, AverageDescriptor>()
                .Include<BinaryOperatorParameters, BinaryDescriptor>()
                .Include<CastOperatorParameters, CastDescriptor>()
                .Include<CeilingOperatorParameters, CeilingDescriptor>()
                .Include<CollectionCastOperatorParameters, CollectionCastDescriptor>()
                .Include<CollectionConstantOperatorParameters, CollectionConstantDescriptor>()
                .Include<CollectionOfTypeOperatorParameters, CollectionOfTypeDescriptor>()
                .Include<ConcatOperatorParameters, ConcatDescriptor>()
                .Include<ConstantOperatorParameters, ConstantDescriptor>()
                .Include<ContainsOperatorParameters, ContainsDescriptor>()
                .Include<ConvertCharArrayToStringOperatorParameters, ConvertCharArrayToStringDescriptor>()
                .Include<ConvertOperatorParameters, ConvertDescriptor>()
                .Include<ConvertToEnumOperatorParameters, ConvertToEnumDescriptor>()
                .Include<ConvertToNullableUnderlyingValueOperatorParameters, ConvertToNullableUnderlyingValueDescriptor>()
                .Include<ConvertToNumericDateOperatorParameters, ConvertToNumericDateDescriptor>()
                .Include<ConvertToNumericTimeOperatorParameters, ConvertToNumericTimeDescriptor>()
                .Include<ConvertToStringOperatorParameters, ConvertToStringDescriptor>()
                .Include<CountOperatorParameters, CountDescriptor>()
                .Include<CustomMethodOperatorParameters, CustomMethodDescriptor>()
                .Include<DateOperatorParameters, DateDescriptor>()
                .Include<DayOperatorParameters, DayDescriptor>()
                .Include<DistinctOperatorParameters, DistinctDescriptor>()
                .Include<DivideBinaryOperatorParameters, DivideBinaryDescriptor>()
                .Include<EndsWithOperatorParameters, EndsWithDescriptor>()
                .Include<EqualsBinaryOperatorParameters, EqualsBinaryDescriptor>()
                .Include<EnumerableSelectorLambdaOperatorParameters, EnumerableSelectorLambdaDescriptor>()
                .Include<ExceptOperatorParameters, ExceptDescriptor>()
                .Include<FilterLambdaOperatorParameters, FilterLambdaDescriptor>()
                .Include<FirstOperatorParameters, FirstDescriptor>()
                .Include<FirstOrDefaultOperatorParameters, FirstOrDefaultDescriptor>()
                .Include<FloorOperatorParameters, FloorDescriptor>()
                .Include<FractionalSecondsOperatorParameters, FractionalSecondsDescriptor>()
                .Include<GreaterThanBinaryOperatorParameters, GreaterThanBinaryDescriptor>()
                .Include<GreaterThanOrEqualsBinaryOperatorParameters, GreaterThanOrEqualsBinaryDescriptor>()
                .Include<GroupByOperatorParameters, GroupByDescriptor>()
                .Include<HasOperatorParameters, HasDescriptor>()
                .Include<HourOperatorParameters, HourDescriptor>()
                .Include<IndexOfOperatorParameters, IndexOfDescriptor>()
                .Include<InOperatorParameters, InDescriptor>()
                .Include<IsOfOperatorParameters, IsOfDescriptor>()
                .Include<LastOperatorParameters, LastDescriptor>()
                .Include<LastOrDefaultOperatorParameters, LastOrDefaultDescriptor>()
                .Include<LengthOperatorParameters, LengthDescriptor>()
                .Include<LessThanBinaryOperatorParameters, LessThanBinaryDescriptor>()
                .Include<LessThanOrEqualsBinaryOperatorParameters, LessThanOrEqualsBinaryDescriptor>()
                .Include<MaxDateTimeOperatorParameters, MaxDateTimeDescriptor>()
                .Include<MaxOperatorParameters, MaxDescriptor>()
                .Include<MemberInitOperatorParameters, MemberInitDescriptor>()
                .Include<MemberSelectorOperatorParameters, MemberSelectorDescriptor>()
                .Include<MinDateTimeOperatorParameters, MinDateTimeDescriptor>()
                .Include<MinOperatorParameters, MinDescriptor>()
                .Include<MinuteOperatorParameters, MinuteDescriptor>()
                .Include<ModuloBinaryOperatorParameters, ModuloBinaryDescriptor>()
                .Include<MonthOperatorParameters, MonthDescriptor>()
                .Include<MultiplyBinaryOperatorParameters, MultiplyBinaryDescriptor>()
                .Include<NegateOperatorParameters, NegateDescriptor>()
                .Include<NotEqualsBinaryOperatorParameters, NotEqualsBinaryDescriptor>()
                .Include<NotOperatorParameters, NotDescriptor>()
                .Include<NowDateTimeOperatorParameters, NowDateTimeDescriptor>()
                .Include<OrBinaryOperatorParameters, OrBinaryDescriptor>()
                .Include<OrderByOperatorParameters, OrderByDescriptor>()
                .Include<ParameterOperatorParameters, ParameterDescriptor>()
                .Include<RoundOperatorParameters, RoundDescriptor>()
                .Include<SecondOperatorParameters, SecondDescriptor>()
                .Include<SelectManyOperatorParameters, SelectManyDescriptor>()
                .Include<SelectOperatorParameters, SelectDescriptor>()
                .Include<SelectorLambdaOperatorParameters, SelectorLambdaDescriptor>()
                .Include<SingleOperatorParameters, SingleDescriptor>()
                .Include<SingleOrDefaultOperatorParameters, SingleOrDefaultDescriptor>()
                .Include<SkipOperatorParameters, SkipDescriptor>()
                .Include<StartsWithOperatorParameters, StartsWithDescriptor>()
                .Include<SubstringOperatorParameters, SubstringDescriptor>()
                .Include<SubtractBinaryOperatorParameters, SubtractBinaryDescriptor>()
                .Include<SumOperatorParameters, SumDescriptor>()
                .Include<TakeOperatorParameters, TakeDescriptor>()
                .Include<ThenByOperatorParameters, ThenByDescriptor>()
                .Include<TimeOperatorParameters, TimeDescriptor>()
                .Include<ToListOperatorParameters, ToListDescriptor>()
                .Include<ToLowerOperatorParameters, ToLowerDescriptor>()
                .Include<TotalOffsetMinutesOperatorParameters, TotalOffsetMinutesDescriptor>()
                .Include<TotalSecondsOperatorParameters, TotalSecondsDescriptor>()
                .Include<ToUpperOperatorParameters, ToUpperDescriptor>()
                .Include<TrimOperatorParameters, TrimDescriptor>()
                .Include<UnionOperatorParameters, UnionDescriptor>()
                .Include<WhereOperatorParameters, WhereDescriptor>()
                .Include<YearOperatorParameters, YearDescriptor>();
        }
    }
}
