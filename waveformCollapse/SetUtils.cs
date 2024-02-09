namespace waveformCollapse;

public static class SetUtils {
    
    public static HashSet<TK> Union<TK>(HashSet<TK> set1, HashSet<TK> set2) {
        return [..set1.Union(set2)];
    }
}