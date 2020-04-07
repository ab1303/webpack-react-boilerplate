const ActionTyper = (prefix = '') =>
  new Proxy(
    {},
    {
      get(target, name) {
        return `${prefix}${name}`;
      },
    },
  );

export default ActionTyper;
