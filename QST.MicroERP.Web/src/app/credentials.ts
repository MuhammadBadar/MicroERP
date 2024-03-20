import { AppConstants } from './app.constants';
export class Credentials {
    GET_CLIENT_ID = localStorage.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID);
    GET_CLIENT_NAME = localStorage.getItem(AppConstants.LOCAL_STORAGE_CLIENT);
    GET_ROLE_ID = localStorage.getItem(AppConstants.LOCAL_STORAGE_ROLE_ID);
    GET_ROLE_NAME = localStorage.getItem(AppConstants.LOCAL_STORAGE_ROLE);
    GET_USER_ID = localStorage.getItem(AppConstants.LOCAL_STORAGE_USER_ID);
    GET_USER_NAME = localStorage.getItem(AppConstants.LOCAL_STORAGE_USER_NAME);
    GET_PERMISSIONS = localStorage.getItem(AppConstants.LOCAL_STORAGE_PERMISSIONS);
    GET_TOKEN = localStorage.getItem(AppConstants.LOCAL_STORAGE_TOKEN);
    GET_SUPERADMIN_ID = localStorage.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_ID);
    GET_SUPERADMIN_NAME = localStorage.getItem(AppConstants.LOCAL_STORAGE_SUPER_ADMIN_NAME);
    GET_DOCTOR_ID = localStorage.getItem(AppConstants.LOCAL_STORAGE_DOCTOR_ID);
    GET_MODULE_IDS = localStorage.getItem(AppConstants.LOCAL_STORAGE_MODULE_IDS);
}