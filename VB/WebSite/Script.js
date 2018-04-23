ArrayHelper = (function() {
    var union = function(target, source) {
        if(!isArray(target))
            return [ ];
        if(!isArray(source))
            source = [ ];
        return distinct(target.concat(source));
    };
    var except = function(target, source) {
        if(!isArray(target))
            return [ ];
        if(!isArray(source))
            return distinct(target);
        var result = [ ];
        var distinctTarget = distinct(target);
        for(var i = 0; i < distinctTarget.length; ++i)
            if(getElementIndex(source, distinctTarget[i]) < 0)
                result.push(distinctTarget[i]);
        return result;
    };
    var intersect = function(target, source) {
        if(!isArray(target))
            return [ ];
        if(!isArray(source))
            return distinct(target);
        var result = [ ];
        var distinctTarget = distinct(target);
        for(var i = 0; i < distinctTarget.length; ++i)
            if(getElementIndex(source, distinctTarget[i]) >= 0)
                result.push(distinctTarget[i]);
        return result;
    };
    var distinct = function(source) {
        if(!isArray(source)) return [ ]; 
        var result = [ ];
        for(var i = 0; i < source.length; ++i)
            if(getElementIndex(result, source[i]) < 0)
                result.push(source[i]);
        return result;
    };
    var getElementIndex = function(source, element) {
        return ASPxClientUtils.IsFunction(source.indexOf) ? source.indexOf(element) : ASPxClientUtils.ArrayIndexOf(source, element);
    };
    var isArray = function(source) {
        return source && source instanceof Array;
    };
    return { 
        Union       : union,
        Except      : except,
        Intersect   : intersect
    };
})();