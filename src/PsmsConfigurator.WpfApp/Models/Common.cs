namespace PsmsConfigurator.WpfApp.Models {
  ///<summary>
  /// Общие настройки
  /// </summary>
  public class Common {
    public Common(bool ledOnNormal, bool firePlumeVerification, bool rfiUsed) {
      LedOnNormalLight = ledOnNormal;
      FirePlumeVerification = firePlumeVerification;
      RfiUsed = rfiUsed;
    }

    ///<summary>
    /// Индикация нормального состояния шлейфов зеленым включена
    /// </summary>
    public bool LedOnNormalLight { get; set; }

    ///<summary>
    /// Верификация состояния пожарных шлейфов включена
    /// </summary>
    public bool FirePlumeVerification { get; set; }

    ///<summary>
    /// Разграничение доступа пометкам RFID включено 
    /// </summary>
    public bool RfiUsed { get; set; }
  }
}