

export abstract class IComponentModel<T> {
  id?: number;
  entityType: number;
  result: T;
}

export abstract class IBaseComponentModel {
  id?: number;
  isActive: boolean;
}
