

export interface IComponentModel<T> {
  id?: number;
  entityType: number;
  result: T;
}

export interface IBaseComponentModel {
  id?: number;
  isActive: boolean;
}
