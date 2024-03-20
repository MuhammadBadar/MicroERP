import { ProductAttribVm } from "./ItemVM";

export class AttributesVm {
    id: number
    name: string
    description: string
    isActive: boolean
    productAttribs?: ProductAttribVm[] = new Array();
}