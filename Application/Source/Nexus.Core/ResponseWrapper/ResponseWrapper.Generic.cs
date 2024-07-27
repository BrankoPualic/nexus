﻿using Nexus.Common;

namespace Nexus.Core;

public class ResponseWrapper<T> : ResponseWrapper
{
	private readonly T _data;

	public ResponseWrapper(Error error) : base(error)
	{ }

	public ResponseWrapper(T data) : base() => _data = data;

	public T Data => IsSuccess ? _data : throw new InvalidOperationException("The value of a response can't be accessed.");
}