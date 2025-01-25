namespace TpLab.ObjectPainter.Editor
{
    public class SettingRepository : SettingRepositoryBase<SettingRepository, ObjectPainterSetting>
    {
        protected override string SettingPath => "Assets/TpLab/ObjectPainter/ObjectPainterSetting.asset";
    }
}
