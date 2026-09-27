using System;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ngnet_native.ViewModels;

namespace ngnet_native;

public interface ICommandService
{
	void SetTitle(SetTitlePayload payload);
}

public record SetTitlePayload(string Value);

public class CommandService(MainViewModel vm) : ICommandService
{
	public void SetTitle(SetTitlePayload payload)
	{
		vm.Title = payload.Value;
	}
}