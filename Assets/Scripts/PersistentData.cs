public static class PersistentData {
    private static string loadLevelName = "PlayLevel1";

    public static void setLoadLevel(string name) {
        loadLevelName = name;
    }

    public static string getLoadLevel() {
        return loadLevelName;
    }
}
