namespace eBroker.ViewModels
{
	public record TraderAddFundVM(int traderId, double balance);

	public record TraderPlaceOrderVM(int traderId, string equityId, int quantity);
}
